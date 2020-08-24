using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.Authentication
{
    public class TokenResponseModel : Data.Entity.Employee
    {
        public string UserToken { get; set; }

        public int TokenExpirePeriod { get; set; }
    }
}
