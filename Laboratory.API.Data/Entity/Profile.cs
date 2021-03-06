﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Laboratory.API.Data.Entity
{
    [Table("Profile")]
    public class Profile
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(150)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public int? DeletedBy { get; set; }

    }
}
