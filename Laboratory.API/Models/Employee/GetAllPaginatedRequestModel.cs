using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.Employee
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        // filters        
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
