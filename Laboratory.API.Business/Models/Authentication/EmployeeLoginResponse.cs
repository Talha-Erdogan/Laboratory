using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Models.Authentication
{
    public class EmployeeLoginResponse
    {
        public bool IsValid { get; set; }
        public Data.Entity.Employee Employee { get; set; }
        public string ErrorMessage { get; set; }
    }
}
