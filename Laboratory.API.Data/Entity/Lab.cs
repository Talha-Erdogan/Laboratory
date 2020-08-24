using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Laboratory.API.Data.Entity
{
    [Table("Lab")]
    public class Lab
    {
        [Key]
        [Column("Id")]
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
