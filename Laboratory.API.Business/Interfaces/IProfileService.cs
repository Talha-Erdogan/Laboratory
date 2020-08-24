using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Profile;
using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface IProfileService
    {
        PaginatedList<Profile> GetAllPaginatedWithDetailBySearchFilter(ProfileSearchFilter searchFilter);
        List<Profile> GetAll();
        Profile GetById(int id);
        int Add(Profile record);
        int Update(Profile record);

    }
}
