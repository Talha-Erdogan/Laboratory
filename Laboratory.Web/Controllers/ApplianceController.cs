using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Common.Enums;
using Laboratory.Web.Business.Enums;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models.Appliance;
using Laboratory.Web.Filters;
using Laboratory.Web.Models.Appliance;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory.Web.Controllers
{
    public class ApplianceController : Controller
    {
        private readonly IApplianceService _applianceService;
        public ApplianceController(IApplianceService applianceService)
        {
            _applianceService = applianceService;
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_APPLIANCE_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            ApplianceSearchFilter searchFilter = new ApplianceSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _applianceService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_APPLIANCE_LIST)]
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

            ApplianceSearchFilter searchFilter = new ApplianceSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _applianceService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_APPLIANCE_ADD)]
        public ActionResult Add()
        {
            Models.Appliance.AddViewModel model = new AddViewModel();
            model.Appliances = new List<ApplianceModel>();
            // boş bir tane cihaz serial bilgisi girme alanı eklendi
            model.Appliances.Add(new ApplianceModel()
            {
                Name = null,
                Barcode = null
            });
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_APPLIANCE_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Appliance.AddViewModel model)
        {
          

            // birden fazla cihaz ekleme butonu
            if (model.SubmitType == "AddApplianceSerial")
            {
                ModelState.Clear();

                if (model.Appliances == null)
                {
                    model.Appliances = new List<ApplianceModel>();
                }

                model.Appliances.Add(new ApplianceModel()
                {
                    Name = null,
                    Barcode = null
                });
              
                return View(model);
            }
            else if (model.SubmitType.StartsWith("DeleteApplianceSerial"))
            {
                int selectedIndex = Convert.ToInt32(model.SubmitType.Replace("DeleteApplianceSerial", ""));

                ModelState.Clear();

                model.Appliances.RemoveAt(selectedIndex);
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            foreach (var item in model.Appliances)
            {
                Business.Models.Appliance.Appliance appliance = new Business.Models.Appliance.Appliance();
                appliance.Name = item.Name;
                appliance.Barcode = item.Barcode;
                var apiResponseModel = _applianceService.Add(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, appliance);
                if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                    ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                    return View(model);
                }
            }
            return RedirectToAction(nameof(ApplianceController.List));
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_APPLIANCE_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Appliance.ApplianceModel model = new ApplianceModel();
            var apiResponseModel = _applianceService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var appliance = apiResponseModel.Data;
            if (appliance == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = appliance.Id;
            model.Name = appliance.Name;
            model.Barcode = appliance.Barcode;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_APPLIANCE_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Appliance.ApplianceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiResponseModel = _applianceService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var appliance = apiResponseModel.Data;

            if (appliance == null)
            {
                return View("_ErrorNotExist");
            }

            appliance.Name = model.Name;
            appliance.Barcode = model.Barcode;
            var apiEditResponseModel = _applianceService.Edit(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, appliance);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                ViewBag.ErrorMessageList = apiEditResponseModel.ErrorMessageList;
                return View(model);
            }


            return RedirectToAction(nameof(ApplianceController.List));
        }
    }
}
