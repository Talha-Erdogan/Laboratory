using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Appliance
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        public string Name { get; set; }
    }
}
