using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Auth
{
    public class Auth
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public int? DeletedBy { get; set; }
    }
}
