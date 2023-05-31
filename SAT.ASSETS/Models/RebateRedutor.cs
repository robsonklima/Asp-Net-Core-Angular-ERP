using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("RebateRedutor")]
    public partial class RebateRedutor
    {
        [Column("NUMEROOS")]
        [StringLength(255)]
        public string Numeroos { get; set; }
        [Column("DTACHAMADA", TypeName = "datetime")]
        public DateTime? Dtachamada { get; set; }
        [Column("DTAFIM", TypeName = "datetime")]
        public DateTime? Dtafim { get; set; }
        [Column("DTALIMITE", TypeName = "datetime")]
        public DateTime? Dtalimite { get; set; }
        [Column("DTAAGENDAMENTO", TypeName = "datetime")]
        public DateTime? Dtaagendamento { get; set; }
        [Column("DENTRODOPRAZO")]
        [StringLength(255)]
        public string Dentrodoprazo { get; set; }
        [Column("CRITICIDADE")]
        [StringLength(10)]
        public string Criticidade { get; set; }
        [Column("MOTIVO")]
        [StringLength(3)]
        public string Motivo { get; set; }
        [Column("DISTANCIA")]
        public int? Distancia { get; set; }
        [Column("REGRA")]
        [StringLength(255)]
        public string Regra { get; set; }
        [Column("GAP00", TypeName = "decimal(10, 2)")]
        public decimal? Gap00 { get; set; }
        [Column("GAP01", TypeName = "decimal(10, 2)")]
        public decimal? Gap01 { get; set; }
        [Column("GAP02", TypeName = "decimal(10, 2)")]
        public decimal? Gap02 { get; set; }
        [Column("GAP03", TypeName = "decimal(10, 2)")]
        public decimal? Gap03 { get; set; }
        [Column("GAP04", TypeName = "decimal(10, 2)")]
        public decimal? Gap04 { get; set; }
        [Column("GAP05", TypeName = "decimal(10, 2)")]
        public decimal? Gap05 { get; set; }
        [Column("GAP06", TypeName = "decimal(10, 2)")]
        public decimal? Gap06 { get; set; }
        [Column("VALORMANUT", TypeName = "decimal(10, 2)")]
        public decimal? Valormanut { get; set; }
    }
}
