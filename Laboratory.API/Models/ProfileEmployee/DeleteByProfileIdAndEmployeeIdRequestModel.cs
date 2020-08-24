using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.ProfileEmployee
{
    public class DeleteByProfileIdAndEmployeeIdRequestModel
    {
        public int ProfileId { get; set; }
        public int EmployeeId { get; set; }
    }
}
