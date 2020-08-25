using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Interfaces
{
    public interface IEmployeeService
    {
        ApiResponseModel<PaginatedList<Employee>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, EmployeeSearchFilter searchFilter);
        ApiResponseModel<Employee> GetById(string userToken, string displayLanguage, int id);
        ApiResponseModel<Employee> Add(string userToken, string displayLanguage, Employee employee);
        ApiResponseModel<Employee> Edit(string userToken, string displayLanguage, Employee employee);
    }
}
