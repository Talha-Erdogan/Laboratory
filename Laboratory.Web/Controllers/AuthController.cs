﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Common.Enums;
using Laboratory.Web.Business.Enums;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models.Auth;
using Laboratory.Web.Filters;
using Laboratory.Web.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
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

            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.CurrentLanguageTwoChar = SessionHelper.CurrentLanguageTwoChar;

            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        public ActionResult Add()
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Business.Models.Auth.Auth auth = new Business.Models.Auth.Auth();
            auth.Code = model.Code;
            auth.Name = model.Name;
            var apiResponseModel = _authService.Add(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, auth);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(AuthController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Kaydedilemedi.";//todo: kulturel olacak NotSaved
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            var apiResponseModel = _authService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var auth = apiResponseModel.Data;
            if (auth == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = auth.Id;
            model.Code = auth.Code;
            model.Name = auth.Name;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiResponseModel = _authService.GetById(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                return View(model);
            }

            var auth = apiResponseModel.Data;

            if (auth == null)
            {
                return View("_ErrorNotExist");
            }

            auth.Code = model.Code;
            auth.Name = model.Name;

            if (model.SubmitType == "Edit")
            {
                var apiEditResponseModel = _authService.Edit(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, auth);
                if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                    ViewBag.ErrorMessageList = apiEditResponseModel.ErrorMessageList;
                    return View(model);
                }
            }
            if (model.SubmitType == "Delete")
            {
                var apiDeleteResponseModel = _authService.Delete(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, auth.Id);
                if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
                {
                    ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage != null ? apiDeleteResponseModel.ResultStatusMessage : "Not Deleted";
                    ViewBag.ErrorMessageList = apiDeleteResponseModel.ErrorMessageList;
                    return View(model);
                }
            }
            return RedirectToAction(nameof(AuthController.List));
        }
    }
}
