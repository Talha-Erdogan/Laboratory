using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Laboratory.API.Data.Entity
{
    [Table("Move")]
    public class Move
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ApplianceId { get; set; }

        [Required]
        public int LabId { get; set; }

        public DateTime EntranceDate { get; set; }

        public DateTime? ExitDate { get; set; }
    }
}
