using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Employee;
using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface IEmployeeService
    {
        PaginatedList<Employee> GetAllPaginatedWithDetailBySearchFilter(EmployeeSearchFilter searchFilter);
        Employee GetByUserNameAndPassword(string userName, string password);
        Employee GetById(int id);
        int Add(Data.Entity.Employee record);
        int Update(Data.Entity.Employee record);



    }
}
