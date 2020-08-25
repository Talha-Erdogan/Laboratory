using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Appliance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Interfaces
{
    public interface IApplianceService
    {
        ApiResponseModel<PaginatedList<Appliance>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, ApplianceSearchFilter searchFilter);
        ApiResponseModel<Appliance> GetById(string userToken, string displayLanguage, int id);
        ApiResponseModel<Appliance> Add(string userToken, string displayLanguage, Appliance appliance);
        ApiResponseModel<Appliance> Edit(string userToken, string displayLanguage, Appliance appliance);
    }
}
