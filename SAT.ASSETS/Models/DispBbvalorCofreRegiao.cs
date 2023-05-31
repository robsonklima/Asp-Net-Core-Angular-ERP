using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBValorCofreRegiao")]
    public partial class DispBbvalorCofreRegiao
    {
        [Column("CodDispBBValorCofreRegiao")]
        public int CodDispBbvalorCofreRegiao { get; set; }
        [Required]
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        public int IndCapital { get; set; }
        public int CodRegiao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
    }
}
