﻿using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Helpers;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Auth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Laboratory.Web.Business
{
   public class AuthService :IAuthService
    {
        //api ile bağlandıgımız servisler
        public ApiResponseModel<PaginatedList<Auth>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, AuthSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Auth>> result = new ApiResponseModel<PaginatedList<Auth>>()
            {
                Data = new PaginatedList<Auth>(new List<Auth>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };
            //portal api'den çekme işlemi            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Auth?CurrentPage={0}&PageSize={1}&SortOn={2}&SortDirection={3}&Code={4}&Name={5}",
                  searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection, searchFilter.Filter_Code, searchFilter.Filter_Name)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<PaginatedList<Auth>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> GetById(string userToken, string displayLanguage, int id)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            // portal api'den çekme işlemi             
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Auth/" + id)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> Add(string userToken, string displayLanguage, Auth auth)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Code = auth.Code;
                portalApiRequestModel.Name = auth.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("v1/Auth"), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> Edit(string userToken, string displayLanguage, Auth auth)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Id = auth.Id;
                portalApiRequestModel.Code = auth.Code;
                portalApiRequestModel.Name = auth.Name;
                HttpResponseMessage response = httpClient.PutAsJsonAsync(string.Format("v1/Auth/" + portalApiRequestModel.Id), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> Delete(string userToken, string displayLanguage, int authId)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                HttpResponseMessage response = httpClient.DeleteAsync(string.Format("v1/Auth/" + authId)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        } //end of delete
    }
}
