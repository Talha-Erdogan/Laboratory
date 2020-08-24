using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Move
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        // filters        
        public string Appliance_Name { get; set; }
        public string Lab_Name { get; set; }
    }
}
