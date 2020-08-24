using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory.API.Models.Move
{
    public class AddRequestModel
    {
        public int Id { get; set; }

        [Required]
        public int ApplianceId { get; set; }

        [Required]
        public int LabId { get; set; }

        public DateTime EntranceDate { get; set; }

        public DateTime? ExitDate { get; set; }
    }
}
