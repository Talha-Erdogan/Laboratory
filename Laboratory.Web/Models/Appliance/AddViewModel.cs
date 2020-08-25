using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.Web.Models.Appliance
{
    public class AddViewModel 
    {
        public List<ApplianceModel> Appliances { get; set; }

        public string SubmitType { get; set; }
    }
}
