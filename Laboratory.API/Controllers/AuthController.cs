using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory.API.Business.Interfaces;
using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Auth;
using Laboratory.API.Common;
using Laboratory.API.Common.Enums;
using Laboratory.API.Common.Model;
using Laboratory.API.Data.Entity;
using Laboratory.API.Filters;
using Laboratory.API.Models;
using Laboratory.API.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("")]
        [HttpGet]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
        public IActionResult GetAllPaginatedWithDetail([FromQuery] GetAllPaginatedRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<PaginatedList<Auth>>();
            responseModel.DisplayLanguage = displayLanguage;

            try
            {
                var searchFilter = new AuthSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Code = requestModel.Code;
                searchFilter.Filter_Name = requestModel.Name;
                responseModel.Data = _authService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);

                responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                responseModel.ResultStatusMessage = "Success";
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
                responseModel.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("{Id}")]
        [HttpGet]
        [TokenAuthorizeFilter]
        public IActionResult GetById(int id, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<Auth>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                responseModel.Data = _authService.GetById(id);
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                responseModel.ResultStatusMessage = "Success";
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
                responseModel.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }


        [HttpPost]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        public IActionResult Add([FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<Auth>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                var record = new Auth();
                record.Code = requestModel.Code;
                record.Name = requestModel.Name;
                record.IsDeleted = false;

                var dbResult = _authService.Add(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                    return Ok(responseModel);
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "Could Not Be Saved";
                    responseModel.Data = null;
                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
                responseModel.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("{Id}")]
        [HttpPut]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        public IActionResult Edit(int id, [FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<Auth>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                var record = _authService.GetById(id);
                record.Code = requestModel.Code;
                record.Name = requestModel.Name;
                var dbResult = _authService.Update(record);
                if (dbResult > 0)
                {
                    responseModel.Data = record;
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                    return Ok(responseModel);
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "Could Not Be Saved";
                    responseModel.Data = null;
                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
                responseModel.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [Route("{Id}")]
        [HttpDelete]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_DELETE)]
        public IActionResult Delete(int id, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<Auth>();
            responseModel.DisplayLanguage = displayLanguage;

            //user bilgilerinden filitre parametreleri eklenir.
            TokenModel tokenModel = TokenHelper.DecodeTokenFromRequestHeader(Request);
            var employeeId = tokenModel.Id;

            try
            {
                var record = _authService.GetById(id);
                record.IsDeleted = true;
                record.DeletedDateTime = DateTime.Now;
                record.DeletedBy = employeeId;
                var dbResult = _authService.Update(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // 'isDeleted= true' yapılan -> entity bilgisi geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                    return Ok(responseModel);
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "Could Not Be Saved";
                    responseModel.Data = null;
                    return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
                responseModel.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

    }
}
