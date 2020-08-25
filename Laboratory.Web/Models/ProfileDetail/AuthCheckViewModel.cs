using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.Web.Models.ProfileDetail
{
    public class AuthCheckViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public string Code { get; set; }
    }
}
