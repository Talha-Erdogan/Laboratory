using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.ProfileEmployee
{
    public class AddRequestModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int EmployeeId { get; set; }
    }
}
