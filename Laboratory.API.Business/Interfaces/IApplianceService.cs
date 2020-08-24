using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Appliance;
using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface IApplianceService
    {
        PaginatedList<Appliance> GetAllPaginatedWithDetailBySearchFilter(ApplianceSearchFilter searchFilter);
        List<Appliance> GetAll();
        Appliance GetById(int id);
        int Add(Appliance record);
        int Update(Appliance record);
    }
}
