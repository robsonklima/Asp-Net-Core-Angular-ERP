using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DispBBRegiao")]
    public partial class DispBbregiao
    {
        [Key]
        [Column("CodDispBBRegiao")]
        public int CodDispBbregiao { get; set; }
        [Required]
        [StringLength(20)]
        public string Nome { get; set; }
        public int IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
    }
}
