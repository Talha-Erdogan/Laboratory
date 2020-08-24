using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.ProfileDetail
{
    public class DeleteByProfileIdAndAuthIdRequestModel
    {
        public int ProfileId { get; set; }
        public int AuthId { get; set; }
    }
}
