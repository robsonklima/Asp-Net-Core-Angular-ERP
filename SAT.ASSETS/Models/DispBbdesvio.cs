using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBDesvio")]
    public partial class DispBbdesvio
    {
        [Column("CodDispBBDesvio")]
        public int CodDispBbdesvio { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal ValInicial { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal ValFinal { get; set; }
        [Column(TypeName = "decimal(10, 5)")]
        public decimal Percentual { get; set; }
        public int IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
    }
}
