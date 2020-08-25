using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Helpers;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Appliance;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Laboratory.Web.Business
{
    public class ApplianceService : IApplianceService
    {
        //api ile bağlandıgımız servisler
        public ApiResponseModel<PaginatedList<Appliance>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, ApplianceSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Appliance>> result = new ApiResponseModel<PaginatedList<Appliance>>()
            {
                Data = new PaginatedList<Appliance>(new List<Appliance>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };
            //portal api'den çekme işlemi            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Appliance?CurrentPage={0}&PageSize={1}&SortOn={2}&SortDirection={3}&Name={4}",
                  searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection, searchFilter.Filter_Name)).Result;

                result = response.Content.ReadAsJsonAsync<ApiResponseModel<PaginatedList<Appliance>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<Appliance>> GetAll(string userToken, string displayLanguage)
        {
            ApiResponseModel<List<Appliance>> result = new ApiResponseModel<List<Appliance>>();
            // portal api'den çekme işlemi 
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Appliance/All")).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<List<Appliance>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Appliance> GetById(string userToken, string displayLanguage, int id)
        {
            ApiResponseModel<Appliance> result = new ApiResponseModel<Appliance>();
            // portal api'den çekme işlemi             
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Appliance/" + id)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Appliance>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Appliance> Add(string userToken, string displayLanguage, Appliance appliance)
        {
            ApiResponseModel<Appliance> result = new ApiResponseModel<Appliance>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Name = appliance.Name;
                portalApiRequestModel.Barcode = appliance.Barcode;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("v1/Appliance"), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Appliance>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Appliance> Edit(string userToken, string displayLanguage, Appliance appliance)
        {
            ApiResponseModel<Appliance> result = new ApiResponseModel<Appliance>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Id = appliance.Id;
                portalApiRequestModel.Name = appliance.Name;
                portalApiRequestModel.Barcode = appliance.Barcode;
                HttpResponseMessage response = httpClient.PutAsJsonAsync(string.Format("v1/Appliance/" + portalApiRequestModel.Id), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Appliance>>().Result;
            }
            return result;
        }
    }
}
