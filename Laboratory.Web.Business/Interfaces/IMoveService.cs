using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Move;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Interfaces
{
    public interface IMoveService
    {
        ApiResponseModel<PaginatedList<MoveWithDetail>> GetAllPaginatedWithDetailBySearchFilter(string userToken, string displayLanguage, MoveSearchFilter searchFilter);
        ApiResponseModel<Move> GetById(string userToken, string displayLanguage, int id);
        ApiResponseModel<Move> Add(string userToken, string displayLanguage, Move move);
        ApiResponseModel<Move> Edit(string userToken, string displayLanguage, Move move);
    }
}
