using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcProdutividadeTecnico
    {
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int? AtendCapital { get; set; }
        [Column("MediaKMCapital")]
        public int? MediaKmcapital { get; set; }
        [Column(TypeName = "decimal(14, 2)")]
        public decimal? PercAtendCapital { get; set; }
        [StringLength(7)]
        public string TempoDirigDiaCapital40KmH { get; set; }
        public int? AtendInterior { get; set; }
        [Column("MediaKMInterior")]
        public int? MediaKminterior { get; set; }
        [Column(TypeName = "decimal(14, 2)")]
        public decimal? PercAtendInterior { get; set; }
        [StringLength(7)]
        public string TempoDirigDiaInterior80KmH { get; set; }
        public int? Atend { get; set; }
        public int? DiasTrab { get; set; }
        public int? Transf { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MediaAtend { get; set; }
        [Required]
        [StringLength(8)]
        public string Localidade { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(7)]
        public string TempoMedioAtend { get; set; }
        [Column("ENG")]
        public int? Eng { get; set; }
        [Column("COR")]
        public int? Cor { get; set; }
        [Column("INS")]
        public int? Ins { get; set; }
        [Column("PRV")]
        public int? Prv { get; set; }
        [Column("ALT")]
        public int? Alt { get; set; }
        public TimeSpan? Ociosidade { get; set; }
        public double? OciosidadeMinutos { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Fator { get; set; }
    }
}
