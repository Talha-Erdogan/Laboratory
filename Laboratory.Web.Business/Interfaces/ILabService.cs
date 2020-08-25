using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Lab;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Interfaces
{
    public interface ILabService
    {
        ApiResponseModel<PaginatedList<Lab>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, LabSearchFilter searchFilter);
        ApiResponseModel<List<Lab>> GetAll(string userToken, string displayLanguage);
        ApiResponseModel<Lab> GetById(string userToken, string displayLanguage, int id); 
        ApiResponseModel<Lab> Add(string userToken, string displayLanguage, Lab lab);
        ApiResponseModel<Lab> Edit(string userToken, string displayLanguage, Lab lab);
    }
}
