﻿using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Helpers;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.ProfileDetail;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Laboratory.Web.Business
{
    public class ProfileDetailService : IProfileDetailService
    {
        public ApiResponseModel<List<Business.Models.Auth.Auth>> GetAllAuthByCurrentUser(string userToken, string displayLanguage, int employeeId)
        {
            ApiResponseModel<List<Business.Models.Auth.Auth>> result = new ApiResponseModel<List<Business.Models.Auth.Auth>>();
            //todo: portal api'den çekme işlemi olacak
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/ProfileDetail/GetAllAuthByCurrentUser?EmployeeId={0}", employeeId)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<List<Business.Models.Auth.Auth>>>().Result;
            }
            return result;
        }
        public ApiResponseModel<List<Business.Models.Auth.Auth>> GetAllAuthByProfileId(string userToken, string displayLanguage, int profileId)
        {
            ApiResponseModel<List<Business.Models.Auth.Auth>> result = new ApiResponseModel<List<Business.Models.Auth.Auth>>();
            //todo: portal api'den çekme işlemi olacak
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/ProfileDetail/GetAllAuthByProfileId?ProfileId={0}", profileId)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<List<Business.Models.Auth.Auth>>>().Result;
            }
            return result;
        }
        public ApiResponseModel<List<Business.Models.Auth.Auth>> GetAllAuthByProfileIdWhichIsNotIncluded(string userToken, string displayLanguage, int profileId)
        {
            ApiResponseModel<List<Business.Models.Auth.Auth>> result = new ApiResponseModel<List<Business.Models.Auth.Auth>>();
            //todo: portal api'den çekme işlemi olacak
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/ProfileDetail/GetAllAuthByProfileIdWhichIsNotIncluded?ProfileId={0}", profileId)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<List<Business.Models.Auth.Auth>>>().Result;
            }
            return result;
        }
        public ApiResponseModel<ProfileDetail> Add(string userToken, string displayLanguage, ProfileDetail profileDetail)
        {
            ApiResponseModel<ProfileDetail> result = new ApiResponseModel<ProfileDetail>();
            // api'yi çağırma yapılır, gelen sonuç elde edilir
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.AuthId = profileDetail.AuthId;
                portalApiRequestModel.ProfileId = profileDetail.ProfileId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("v1/ProfileDetail"), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<ProfileDetail>>().Result;
            }
            return result;
        }
        public ApiResponseModel<int> DeleteByProfileIdAndAuthId(string userToken, string displayLanguage, int profileId, int authId)
        {
            ApiResponseModel<int> result = new ApiResponseModel<int>();
            // api'yi çağırma yapılır, gelen sonuç elde edilir
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.DeleteAsync(string.Format("v1/ProfileDetail?ProfileId={0}&AuthId={1}", profileId, authId)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<int>>().Result;
            }
            return result;
        }
    }
}
