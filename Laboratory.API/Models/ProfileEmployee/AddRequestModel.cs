using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.ProfileEmployee
{
    public class AddRequestModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int EmployeeId { get; set; }
    }
}
