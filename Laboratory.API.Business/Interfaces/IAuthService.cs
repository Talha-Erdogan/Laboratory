using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Auth;
using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface IAuthService
    {
        PaginatedList<Auth> GetAllPaginatedWithDetailBySearchFilter(AuthSearchFilter searchFilter);
        List<Auth> GetAll();
        Auth GetById(int id);
        int Add(Auth record);
        int Update(Auth record);

    }
}
