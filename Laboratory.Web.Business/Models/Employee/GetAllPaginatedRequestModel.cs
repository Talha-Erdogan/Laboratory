﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Employee
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        // filters        
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
