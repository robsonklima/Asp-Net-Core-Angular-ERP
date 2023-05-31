using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBPercRegiao")]
    public partial class DispBbpercRegiao
    {
        [Column("CodDispBBPercRegiao")]
        public int CodDispBbpercRegiao { get; set; }
        [Column("CodDispBBRegiao")]
        public int? CodDispBbregiao { get; set; }
        public int Criticidade { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal Percentual { get; set; }
        public int IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
    }
}
