﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.Lab
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
