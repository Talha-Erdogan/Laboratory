using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Laboratory.Web.Business.Models.Appliance
{
    public class AddRequestModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Barcode { get; set; }
    }
}
