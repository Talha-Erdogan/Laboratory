using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory.API.Business.Interfaces;
using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Move;
using Laboratory.API.Common.Enums;
using Laboratory.API.Data.Entity;
using Laboratory.API.Models;
using Laboratory.API.Models.Move;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly IMoveService _moveService;
        public MoveController(IMoveService moveService)
        {
            _moveService = moveService;
        }


        [Route("")]
        [HttpGet]
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_LIST)]
        public IActionResult GetAllPaginatedWithDetail([FromQuery] GetAllPaginatedRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<PaginatedList<MoveWithDetail>>();
            responseModel.DisplayLanguage = displayLanguage;

            try
            {
                var searchFilter = new MoveSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Appliance_Name = requestModel.Appliance_Name;
                searchFilter.Filter_Lab_Name = requestModel.Lab_Name;
                responseModel.Data = _moveService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);

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
        //[TokenAuthorizeFilter]
        public IActionResult GetById(int id, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<Move>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                responseModel.Data = _moveService.GetById(id);
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
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_ADD)]
        public IActionResult Add([FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<Move>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                var record = new Move();
                record.ApplianceId = requestModel.ApplianceId;
                record.LabId = requestModel.LabId;
                record.EntranceDate = requestModel.EntranceDate;
                record.ExitDate = requestModel.ExitDate;

                var dbResult = _moveService.Add(record);

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
        //[TokenAuthorizeFilter(AuthCodeStatic.PAGE_MOVE_EDIT)]
        public IActionResult Edit(int id, [FromBody] AddRequestModel requestModel, [FromHeader] string displayLanguage)
        {
            var responseModel = new ApiResponseModel<Move>();
            responseModel.DisplayLanguage = displayLanguage;
            try
            {
                var record = _moveService.GetById(id);
                record.ExitDate = DateTime.Now;
                var dbResult = _moveService.Update(record);
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


    }
}
