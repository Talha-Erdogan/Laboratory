using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Common.Enums;
using Laboratory.Web.Business.Enums;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models.Employee;
using Laboratory.Web.Filters;
using Laboratory.Web.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_EMPLOYEE_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            EmployeeSearchFilter searchFilter = new EmployeeSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _employeeService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_EMPLOYEE_LIST)]
        [HttpPost]
        public ActionResult List(ListViewModel model)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (model.Filter == null)
            {
                model.Filter = new ListFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }

            EmployeeSearchFilter searchFilter = new EmployeeSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _employeeService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_EMPLOYEE_ADD)]
        public ActionResult Add()
        {
            Models.Employee.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_EMPLOYEE_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Employee.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Business.Models.Employee.Employee employee = new Business.Models.Employee.Employee();
            employee.TC = model.TC;
            employee.Name = model.Name;
            employee.LastName = model.LastName;
            employee.Phone = model.Phone;
            employee.Address = model.Address;
            employee.UserName = model.UserName;
            employee.Password = model.Password;
            var apiResponseModel = _employeeService.Add(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, employee);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(EmployeeController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_EMPLOYEE_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Employee.AddViewModel model = new AddViewModel();
            var apiResponseModel = _employeeService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var employee = apiResponseModel.Data;
            if (employee == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = employee.Id;
            model.TC = employee.TC;
            model.Name = employee.Name;
            model.LastName = employee.LastName;
            model.Phone = employee.Phone;
            model.Address = employee.Address;
            model.UserName = employee.UserName;
            model.Password = employee.Password;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_EMPLOYEE_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Employee.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiResponseModel = _employeeService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var employee = apiResponseModel.Data;

            if (employee == null)
            {
                return View("_ErrorNotExist");
            }

            employee.TC = model.TC;
            employee.Name = model.Name;
            employee.LastName = model.LastName;
            employee.Phone = model.Phone;
            employee.Address = model.Address;
            employee.UserName = model.UserName;
            employee.Password = model.Password;


            var apiEditResponseModel = _employeeService.Edit(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, employee);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                ViewBag.ErrorMessageList = apiEditResponseModel.ErrorMessageList;
                return View(model);
            }

            return RedirectToAction(nameof(EmployeeController.List));
        }
    }
}
