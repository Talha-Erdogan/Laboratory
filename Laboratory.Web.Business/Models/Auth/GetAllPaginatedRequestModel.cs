using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Auth
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
