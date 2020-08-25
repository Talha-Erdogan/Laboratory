using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Helpers;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Move;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Laboratory.Web.Business
{
    public class MoveService : IMoveService
    {
        //api ile bağlandıgımız servisler
        public ApiResponseModel<PaginatedList<Move>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, MoveSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Move>> result = new ApiResponseModel<PaginatedList<Move>>()
            {
                Data = new PaginatedList<Move>(new List<Move>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };
            //portal api'den çekme işlemi            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Lab?CurrentPage={0}&PageSize={1}&SortOn={2}&SortDirection={3}&Appliance_Name={4}&Lab_Name",
                  searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection, searchFilter.Filter_Appliance_Name,searchFilter.Filter_Lab_Name)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<PaginatedList<Move>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Move> GetById(string userToken, string displayLanguage, int id)
        {
            ApiResponseModel<Move> result = new ApiResponseModel<Move>();
            // portal api'den çekme işlemi             
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Move/" + id)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Move>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Move> Add(string userToken, string displayLanguage, Move move)
        {
            ApiResponseModel<Move> result = new ApiResponseModel<Move>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.ApplianceId = move.ApplianceId;
                portalApiRequestModel.LabId = move.LabId;
                portalApiRequestModel.EntranceDate = move.EntranceDate;
                portalApiRequestModel.ExitDate = move.ExitDate;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("v1/Move"), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Move>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Move> Edit(string userToken, string displayLanguage, Move move)
        {
            ApiResponseModel<Move> result = new ApiResponseModel<Move>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Id = move.Id;
                portalApiRequestModel.ApplianceId = move.ApplianceId;
                portalApiRequestModel.LabId = move.LabId;
                portalApiRequestModel.EntranceDate = move.EntranceDate;
                portalApiRequestModel.ExitDate = move.ExitDate;
                HttpResponseMessage response = httpClient.PutAsJsonAsync(string.Format("v1/Move/" + portalApiRequestModel.Id), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Move>>().Result;
            }
            return result;
        }
    }
}
