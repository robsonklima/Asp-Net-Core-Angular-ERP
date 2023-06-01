using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORModal")]
    public partial class Ormodal
    {
        [Key]
        public int CodModal { get; set; }
        [Required]
        [StringLength(50)]
        public string TipoModal { get; set; }
    }
}
