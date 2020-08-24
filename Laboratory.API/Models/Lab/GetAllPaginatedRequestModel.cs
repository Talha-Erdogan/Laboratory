using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.Lab
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        public string Name { get; set; }
    }
}
