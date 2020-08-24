using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Lab;
using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface ILabService
    {
        PaginatedList<Lab> GetAllPaginatedWithDetailBySearchFilter(LabSearchFilter searchFilter);
        List<Lab> GetAll();
        Lab GetById(int id);
        int Add(Lab record);
        int Update(Lab record);
    }
}
