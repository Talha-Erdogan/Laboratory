﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Profile
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        public string Code { get; set; }
        public string NameTR { get; set; }
        public string NameEN { get; set; }
    }
}
