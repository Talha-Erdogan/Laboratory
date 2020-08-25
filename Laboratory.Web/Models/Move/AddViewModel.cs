using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.Web.Models.Move
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ApplianceId { get; set; }

        [Required]
        public int LabId { get; set; }

        public DateTime EntranceDate { get; set; }

        public DateTime? ExitDate { get; set; }



        //selectList
        public List<SelectListItem> ApplianceSelectList { get; set; }
        public List<SelectListItem> LabSelectList { get; set; }
    }
}
