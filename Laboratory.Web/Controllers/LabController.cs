using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Common.Enums;
using Laboratory.Web.Business.Enums;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models.Lab;
using Laboratory.Web.Filters;
using Laboratory.Web.Models.Lab;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory.Web.Controllers
{
    public class LabController : Controller
    {
        private readonly ILabService _labService;
        public LabController(ILabService labService)
        {
            _labService = labService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_LAB_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            LabSearchFilter searchFilter = new LabSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _labService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_LAB_LIST)]
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

            LabSearchFilter searchFilter = new LabSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _labService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_LAB_ADD)]
        public ActionResult Add()
        {
            Models.Lab.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_LAB_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Lab.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Business.Models.Lab.Lab lab = new Business.Models.Lab.Lab();
            lab.Name = model.Name;
            lab.MaxApplianceCapacity = model.MaxApplianceCapacity;
            var apiResponseModel = _labService.Add(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, lab);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(LabController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_LAB_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Lab.AddViewModel model = new AddViewModel();
            var apiResponseModel = _labService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var lab = apiResponseModel.Data;
            if (lab == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = lab.Id;
            model.Name = lab.Name;
            model.MaxApplianceCapacity = lab.MaxApplianceCapacity;
            model.CurrentApplianceCapacity = lab.CurrentApplianceCapacity;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_LAB_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Lab.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiResponseModel = _labService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var lab = apiResponseModel.Data;

            if (lab == null)
            {
                return View("_ErrorNotExist");
            }

            lab.Name = model.Name;
            lab.MaxApplianceCapacity = model.MaxApplianceCapacity;


            var apiEditResponseModel = _labService.Edit(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, lab);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                ViewBag.ErrorMessageList = apiEditResponseModel.ErrorMessageList;
                return View(model);
            }

            return RedirectToAction(nameof(LabController.List));
        }
    }
}
