using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Laboratory.API.Data.Entity
{
    [Table("ProfileEmployee")]
    public class ProfileEmployee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
    }
}
