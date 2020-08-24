using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Authentication
{
    public class LoginResponseModel : Employee.Employee
    {
        public string UserToken { get; set; }
        public int TokenExpirePeriod { get; set; }
    }
}
