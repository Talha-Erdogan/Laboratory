using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Laboratory.Web.Business.Models.Lab
{
    public class AddRequestModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int MaxApplianceCapacity { get; set; }

        [Required]
        public int CurrentApplianceCapacity { get; set; }
    }
}
