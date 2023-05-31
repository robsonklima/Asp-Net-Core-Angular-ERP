using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrigemChamadoSTN")]
    public partial class OrigemChamadoStn
    {
        [Key]
        [Column("CodOrigemChamadoSTN")]
        public int CodOrigemChamadoStn { get; set; }
        [Required]
        [Column("DescOrigemChamadoSTN")]
        [StringLength(100)]
        public string DescOrigemChamadoStn { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}
