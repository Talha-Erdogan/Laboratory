﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Common.Enums;
using Laboratory.Web.Business.Enums;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models.ProfileDetail;
using Laboratory.Web.Models.ProfileDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Laboratory.Web.Controllers
{
    public class ProfileDetailController : Controller
    {
        private readonly IProfileDetailService _profileDetailService;
        private readonly IProfileService _profileService;

        public ProfileDetailController(IProfileDetailService profileDetailService, IProfileService profileService)
        {
            _profileDetailService = profileDetailService;
            _profileService = profileService;
        }

       // [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILEDETAIL_BATCHEDIT)]
        public ActionResult BatchEdit()
        {
            BatchEditViewModel model = new BatchEditViewModel();
            model.ProfileSelectList = GetProfileSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
            model.AuthList = new List<AuthCheckViewModel>();
            model.AuthWhichIsNotIncludeList = new List<AuthCheckViewModel>();
            return View(model);
        }

        [HttpPost]
        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILEDETAIL_BATCHEDIT)]
        public ActionResult BatchEdit(BatchEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.ProfileId.HasValue)
                {
                    model.AuthList = GetAllAuthByProfileId(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.ProfileId.Value);
                    model.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.ProfileId.Value);
                }
                else
                {
                    model.AuthList = new List<AuthCheckViewModel>();
                    model.AuthWhichIsNotIncludeList = new List<AuthCheckViewModel>();
                }
                return View(model);
            }

            if (model.SubmitType == "Add")
            {
                if (model.AuthWhichIsNotIncludeList != null)
                {
                    ModelState.Clear();
                    List<AuthCheckViewModel> record = model.AuthWhichIsNotIncludeList.Where(x => x.Checked == true).ToList();
                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                            ProfileDetail profileDetail = new ProfileDetail();
                            profileDetail.AuthId = item.Id;
                            profileDetail.ProfileId = model.ProfileId.Value;
                            _profileDetailService.Add(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, profileDetail);
                        }
                    }
                }
            }
            if (model.SubmitType == "Delete")
            {
                if (model.AuthList != null)
                {
                    ModelState.Clear();
                    List<AuthCheckViewModel> record = model.AuthList.Where(x => x.Checked == true).ToList();
                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                            var apiResponseModel = _profileDetailService.DeleteByProfileIdAndAuthId(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.ProfileId.Value, item.Id);
                            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
                            {
                                //not error
                            }
                            else
                            {
                                BatchEditViewModel batchEditViewModel = new BatchEditViewModel();

                                batchEditViewModel.ProfileSelectList = GetProfileSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);


                                batchEditViewModel.AuthList = GetAllAuthByProfileId(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.ProfileId.Value);
                                batchEditViewModel.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.ProfileId.Value);
                                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                                ViewBag.ErrorMessageList = apiResponseModel.ErrorMessageList;
                                return View(batchEditViewModel);
                            }
                        }
                    }
                }
            }


            model.ProfileSelectList = GetProfileSelectList(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar);
            if (model.ProfileId.HasValue)
            {
                model.AuthList = GetAllAuthByProfileId(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.ProfileId.Value);
                model.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, model.ProfileId.Value);
            }
            else
            {
                model.AuthList = new List<AuthCheckViewModel>();
                model.AuthWhichIsNotIncludeList = new List<AuthCheckViewModel>();
            }
            return View(model);
        }

        [NonAction]
        private List<AuthCheckViewModel> GetAllAuthByProfileId(string userToken, string displayLanguage, int profileId)
        {
            //profile ait yetki kayıtları listelenir
            List<AuthCheckViewModel> resultList = new List<AuthCheckViewModel>();
            // api'den çekim yapılacak
            var apiResponseModel = _profileDetailService.GetAllAuthByProfileId(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, profileId);
            resultList = apiResponseModel.Data.Select(a => new AuthCheckViewModel() { Id = a.Id, Name = a.Name, Checked = false, Code = a.Code }).ToList();
            return resultList;
        }

        [NonAction]
        private List<AuthCheckViewModel> GetAllAuthByProfileIdWhichIsNotIncluded(string userToken, string displayLanguage, int profileId)
        {
            //profile ait olmayan yetki kayıtları listelenir
            List<AuthCheckViewModel> resultList = new List<AuthCheckViewModel>();
            // api'den çekim yapılacak
            var apiResponseModel = _profileDetailService.GetAllAuthByProfileIdWhichIsNotIncluded(SessionHelper.CurrentUser.UserToken, SessionHelper.CurrentLanguageTwoChar, profileId);
            resultList = apiResponseModel.Data.Select(a => new AuthCheckViewModel() { Id = a.Id, Name =  a.Name, Checked = false, Code = a.Code }).ToList();
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetProfileSelectList(string userToken, string displayLanguage)
        {
            // aktif profil kayıtları listelenir
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _profileService.GetAll(userToken, displayLanguage);
            resultList = apiResponseModel.Data.OrderBy(r =>  r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text =  r.Name }).ToList();
            return resultList;
        }
    }
}
