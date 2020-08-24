using Laboratory.Web.Business.Models;
using Laboratory.Web.Business.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Interfaces
{
    public interface IAuthenticationService
    {
        ApiResponseModel<LoginResponseModel> Login(string username, string password, string displayLanguage);

    }
}
