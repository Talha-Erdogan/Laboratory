using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.Move
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        // filters        
        public string Appliance_Name { get; set; }
        public string Lab_Name { get; set; }
    }
}
