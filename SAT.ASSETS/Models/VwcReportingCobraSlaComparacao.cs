using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingCobraSlaComparacao
    {
        [Column("OS")]
        public int Os { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Required]
        [StringLength(10)]
        public string AnalisePlanilhaCobra { get; set; }
        [StringLength(7)]
        public string TipoRetornoCliente { get; set; }
        [Required]
        [Column("SEMAT")]
        [StringLength(3)]
        public string Semat { get; set; }
        [Required]
        [StringLength(3)]
        public string PontoEstrategico { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("StatusOS")]
        [StringLength(50)]
        public string StatusOs { get; set; }
        [Column("DataAberturaOS", TypeName = "datetime")]
        public DateTime? DataAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAberturaCobra { get; set; }
        [Required]
        [Column("AnaliseAberturaOS")]
        [StringLength(12)]
        public string AnaliseAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamentoCobra { get; set; }
        [Required]
        [StringLength(26)]
        public string AnaliseDataAgendamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFechamentoCobra { get; set; }
        [Required]
        [StringLength(9)]
        public string AnaliseDataFechamento { get; set; }
        [Column("DataFimSLA", TypeName = "datetime")]
        public DateTime? DataFimSla { get; set; }
        [Column("DataFimSLACobra", TypeName = "datetime")]
        public DateTime? DataFimSlacobra { get; set; }
        [Required]
        [Column("AnaliseDataFimSLA")]
        [StringLength(9)]
        public string AnaliseDataFimSla { get; set; }
        [Column("DataFimSLA_SemVand")]
        [StringLength(20)]
        public string DataFimSlaSemVand { get; set; }
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
        [Column("KM")]
        public int? Km { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Required]
        [Column("StatusSLACobra")]
        [StringLength(6)]
        public string StatusSlacobra { get; set; }
        [Required]
        [Column("AnaliseStatusSLA")]
        [StringLength(9)]
        public string AnaliseStatusSla { get; set; }
        [Column("QtdHorasAcrescimoKM")]
        public string QtdHorasAcrescimoKm { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal? ManutencaoMensal { get; set; }
        [Required]
        [Column("CL10_1_5")]
        [StringLength(3)]
        public string Cl1015 { get; set; }
    }
}
