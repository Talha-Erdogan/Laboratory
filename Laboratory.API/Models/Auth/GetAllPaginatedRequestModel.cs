using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.Auth
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
