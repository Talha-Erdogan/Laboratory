using Laboratory.Web.Business.Common;
using Laboratory.Web.Business.Helpers;
using Laboratory.Web.Business.Interfaces;
using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Employee;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Laboratory.Web.Business
{
    public class EmployeeService : IEmployeeService
    {
        //api ile bağlandıgımız servisler
        public ApiResponseModel<PaginatedList<Employee>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, EmployeeSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Employee>> result = new ApiResponseModel<PaginatedList<Employee>>()
            {
                Data = new PaginatedList<Employee>(new List<Employee>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };
            //portal api'den çekme işlemi            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);
                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Employee?CurrentPage={0}&PageSize={1}&SortOn={2}&SortDirection={3}&Name={4}&LastName={5}",
                  searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection, searchFilter.Filter_Name, searchFilter.Filter_LastName)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<PaginatedList<Employee>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Employee> GetById(string userToken, string displayLanguage, int id)
        {
            ApiResponseModel<Employee> result = new ApiResponseModel<Employee>();
            // portal api'den çekme işlemi             
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                HttpResponseMessage response = httpClient.GetAsync(string.Format("v1/Employee/" + id)).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Employee>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Employee> Add(string userToken, string displayLanguage, Employee employee)
        {
            ApiResponseModel<Employee> result = new ApiResponseModel<Employee>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.TC = employee.TC;
                portalApiRequestModel.Name = employee.Name;
                portalApiRequestModel.LastName = employee.LastName;
                portalApiRequestModel.Phone = employee.Phone;
                portalApiRequestModel.Address = employee.Address;
                portalApiRequestModel.UserName = employee.UserName;
                portalApiRequestModel.Password = employee.Password;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("v1/Employee"), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Employee>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Employee> Edit(string userToken, string displayLanguage, Employee employee)
        {
            ApiResponseModel<Employee> result = new ApiResponseModel<Employee>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                httpClient.DefaultRequestHeaders.Add("DisplayLanguage", displayLanguage);

                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.Id = employee.Id;
                portalApiRequestModel.TC = employee.TC;
                portalApiRequestModel.Name = employee.Name;
                portalApiRequestModel.LastName = employee.LastName;
                portalApiRequestModel.Phone = employee.Phone;
                portalApiRequestModel.Address = employee.Address;
                portalApiRequestModel.UserName = employee.UserName;
                portalApiRequestModel.Password = employee.Password;
                HttpResponseMessage response = httpClient.PutAsJsonAsync(string.Format("v1/Employee/" + portalApiRequestModel.Id), portalApiRequestModel).Result;
                result = response.Content.ReadAsJsonAsync<ApiResponseModel<Employee>>().Result;
            }
            return result;
        }
    }
}
