using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Helpers;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Lab;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Laboratory.Web.Business
{
    public class LabService : ILabService
    {
        //api ile bağlandıgımız servisler
        public ApiResponseModel<PaginatedList<Lab>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, LabSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Lab>> result = new ApiResponseModel<PaginatedList<Lab>>()
            {
                Data = new PaginatedList<Lab>(new List<Lab>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };
            //portal api'den çekme işlemi            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Lab?CurrentPage={0}&PageSize={1}&SortOn={2}&SortDirection={3}&Name={4}",
                  searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection,  searchFilter.Filter_Name)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<PaginatedList<Lab>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Lab> GetById(string userToken, string displayLanguage, int id)
        {
            ApiResponseModel<Lab> result = new ApiResponseModel<Lab>();
            // portal api'den çekme işlemi             
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Lab/" + id)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Lab>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Lab> Add(string userToken, string displayLanguage, Lab lab)
        {
            ApiResponseModel<Lab> result = new ApiResponseModel<Lab>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Name = lab.Name;
                portalApiRequestModel.CurrentApplianceCapacity = lab.CurrentApplianceCapacity;
                portalApiRequestModel.MaxApplianceCapacity = lab.MaxApplianceCapacity;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("v1/Lab"), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Lab>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Lab> Edit(string userToken, string displayLanguage, Lab lab)
        {
            ApiResponseModel<Lab> result = new ApiResponseModel<Lab>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Id = lab.Id;
                portalApiRequestModel.Name = lab.Name;
                portalApiRequestModel.CurrentApplianceCapacity = lab.CurrentApplianceCapacity;
                portalApiRequestModel.MaxApplianceCapacity = lab.MaxApplianceCapacity;
                HttpResponseMessage response = httpClient.PutAsJsonAsync(string.Format("v1/Lab/" + portalApiRequestModel.Id), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Lab>>().Result;
            }
            return result;
        }
    }
}
