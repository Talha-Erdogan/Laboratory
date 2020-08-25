using System;
using System.Collections.Generic;
using System.Linq;
using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Common.Enums;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models.Move;
using Laboratory.Web.Models.Move;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Laboratory.Web.Controllers
{
    public class MoveController : Controller
    {
        private readonly IMoveService _moveService;
        private readonly IApplianceService _applianceService;
        private readonly ILabService _labService;
        public MoveController(IMoveService moveService, IApplianceService applianceService, ILabService labService)
        {
            _moveService = moveService;
            _applianceService = applianceService;
            _labService = labService;
        }


        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_LIST)]
        public ActionResult List(string errorMessage=default)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            MoveSearchFilter searchFilter = new MoveSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Appliance_Name = model.Filter.Filter_Appliance_Name;
            searchFilter.Filter_Lab_Name = model.Filter.Filter_Lab_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _moveService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_LIST)]
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

            MoveSearchFilter searchFilter = new MoveSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Appliance_Name = model.Filter.Filter_Appliance_Name;
            searchFilter.Filter_Lab_Name = model.Filter.Filter_Lab_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _moveService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_ADD)]
        public ActionResult Add()
        {
            Models.Move.AddViewModel model = new AddViewModel();
            //select list
            model.ApplianceSelectList = GetApplianceSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
            model.LabSelectList =GetLabSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Move.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select list
                model.ApplianceSelectList = GetApplianceSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
                model.LabSelectList = GetLabSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
                return View(model);
            }

            Business.Models.Move.Move move = new Business.Models.Move.Move();
            move.ApplianceId = model.ApplianceId;
            move.LabId = model.LabId;
            move.EntranceDate = DateTime.Now;

            var apiResponseModel = _moveService.Add(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, move);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(MoveController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                //select list
                model.ApplianceSelectList = GetApplianceSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
                model.LabSelectList = GetLabSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
                return View(model);
            }
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_EDIT)]
        public ActionResult Edit(int id)
        {
           
            var apiResponseModel = _moveService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return RedirectToAction(nameof(MoveController.List),new { errorMessage ="Hata." });
            }

            var move = apiResponseModel.Data;
            if (move == null)
            {
                return View("_ErrorNotExist");
            }
            move.ExitDate = DateTime.Now;
            var apiEditResponseModel = _moveService.Edit(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, move);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                ViewBag.ErrorMessageList = apiEditResponseModel.ErrorMessageList;
                return RedirectToAction(nameof(MoveController.List), new { errorMessage = "Not Edited." });
            }
            return RedirectToAction(nameof(MoveController.List));
        }

        [NonAction]
        private List<SelectListItem> GetApplianceSelectList(string userToken, string displayLanguage)
        {
            // aktif cihaz kayıtları listelenir
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _applianceService.GetAll(userToken, displayLanguage);
            resultList = apiResponseModel.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetLabSelectList(string userToken, string displayLanguage)
        {
            // aktif lab kayıtları listelenir
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _labService.GetAll(userToken, displayLanguage);
            resultList = apiResponseModel.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }
    }
}
