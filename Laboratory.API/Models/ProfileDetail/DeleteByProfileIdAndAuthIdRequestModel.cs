using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.ProfileDetail
{
    public class DeleteByProfileIdAndAuthIdRequestModel
    {
        public int ProfileId { get; set; }
        public int AuthId { get; set; }
    }
}
