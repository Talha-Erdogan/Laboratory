using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Move;
using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface IMoveService
    {
        PaginatedList<MoveWithDetail> GetAllPaginatedWithDetailBySearchFilter(MoveSearchFilter searchFilter);
        List<Move> GetAll();
        Move GetById(int id);
        MoveWithDetail GetByIdWithDetail(int id);
        int Add(Move record);
        int Update(Move record);
    }
}
