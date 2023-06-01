using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBRebate")]
    public partial class DispBbrebate
    {
        [Column("PERIODO")]
        [StringLength(7)]
        public string Periodo { get; set; }
        [Column("NUMEROOS")]
        [StringLength(255)]
        public string Numeroos { get; set; }
        [Column("UF")]
        [StringLength(2)]
        public string Uf { get; set; }
        [Column("REGIAO")]
        [StringLength(2)]
        public string Regiao { get; set; }
        [Column("CRITICIDADE")]
        [StringLength(10)]
        public string Criticidade { get; set; }
        [Column("NUMEROBEM")]
        [StringLength(255)]
        public string Numerobem { get; set; }
        [Column("DESCRICAOBEM")]
        [StringLength(255)]
        public string Descricaobem { get; set; }
        [Column("NROCONTRATO")]
        [StringLength(255)]
        public string Nrocontrato { get; set; }
        [Column("DTACHAMADA", TypeName = "datetime")]
        public DateTime? Dtachamada { get; set; }
        [Column("DTAREFERENCIA", TypeName = "datetime")]
        public DateTime? Dtareferencia { get; set; }
        [Column("DTAFIM", TypeName = "datetime")]
        public DateTime? Dtafim { get; set; }
        [Column("TEMPO_SOLUCAO_BBTS_HORAS", TypeName = "decimal(10, 2)")]
        public decimal? TempoSolucaoBbtsHoras { get; set; }
        [Column("MANTENEDOR")]
        [StringLength(5)]
        public string Mantenedor { get; set; }
        [Column("MOTIVO")]
        [StringLength(5)]
        public string Motivo { get; set; }
    }
}
