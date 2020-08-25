using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.Web.Models.Lab
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int MaxApplianceCapacity { get; set; }

        [Required]
        public int CurrentApplianceCapacity { get; set; }
    }
}
