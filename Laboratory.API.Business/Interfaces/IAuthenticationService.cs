using Laboratory.API.Business.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface IAuthenticationService
    {
        EmployeeLoginResponse Login(string userName, string password, string language);
    }
}
