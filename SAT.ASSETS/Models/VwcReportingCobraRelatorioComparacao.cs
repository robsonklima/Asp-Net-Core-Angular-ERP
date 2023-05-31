using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingCobraRelatorioComparacao
    {
        [Column("OS")]
        public int Os { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        [Required]
        [StringLength(10)]
        public string AnalisePlanilhaCobra { get; set; }
        [Required]
        [StringLength(7)]
        public string TipoRetornoCliente { get; set; }
        [Required]
        [Column("SEMAT")]
        [StringLength(3)]
        public string Semat { get; set; }
        [Required]
        [StringLength(3)]
        public string PontoEstrategico { get; set; }
        [Column("StatusOS")]
        [StringLength(50)]
        public string StatusOs { get; set; }
        [Column("QtdHorasAcrescimoKM")]
        public string QtdHorasAcrescimoKm { get; set; }
        [Required]
        [Column("CL10_1_5")]
        [StringLength(3)]
        public string Cl1015 { get; set; }
        [Required]
        [StringLength(3)]
        public string Vandalismo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEnvioFechamento { get; set; }
        [Column("NomeArquivoRAT")]
        [StringLength(500)]
        public string NomeArquivoRat { get; set; }
        [Column("DataEnvioPND", TypeName = "datetime")]
        public DateTime? DataEnvioPnd { get; set; }
        [Column("NomeArquivoPND")]
        [StringLength(500)]
        public string NomeArquivoPnd { get; set; }
        [Required]
        [StringLength(3)]
        public string ChamadoReaberto { get; set; }
        [Required]
        [StringLength(3)]
        public string DataAlterada { get; set; }
        [Required]
        [StringLength(3)]
        public string ChamadoCancelado { get; set; }
        [Required]
        [StringLength(3)]
        public string IntervençãoNãoCorretiva { get; set; }
        [Required]
        [StringLength(3)]
        public string EquipamentoForadeGarantia { get; set; }
        [Required]
        [StringLength(3)]
        public string Contingencia { get; set; }
        [Required]
        [Column("SLAForadeContrato")]
        [StringLength(3)]
        public string SlaforadeContrato { get; set; }
        public int CodCidade { get; set; }
        public int? Regiao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioFim { get; set; }
        [Required]
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("IndRHorario")]
        public byte? IndRhorario { get; set; }
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
        [Column("HORAS")]
        [StringLength(7)]
        public string Horas { get; set; }
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
        [Column("KMPerto", TypeName = "decimal(10, 2)")]
        public decimal? Kmperto { get; set; }
        [Column("KMCobra")]
        public int? Kmcobra { get; set; }
        [Required]
        [Column("AnaliseKM")]
        [StringLength(3)]
        public string AnaliseKm { get; set; }
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
        [Column("SLAPerto")]
        [StringLength(50)]
        public string Slaperto { get; set; }
        [Column("SLACobra")]
        [StringLength(10)]
        public string Slacobra { get; set; }
        [Required]
        [Column("AnaliseSLA")]
        [StringLength(9)]
        public string AnaliseSla { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal? ManutencaoMensalPerto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ManutencaoMensalCobra { get; set; }
        [Required]
        [StringLength(9)]
        public string AnaliseValorManutencao { get; set; }
        [StringLength(255)]
        public string RegraCobra { get; set; }
        public string RegraPerto { get; set; }
        [Column("REBATE - Perto", TypeName = "numeric(10, 2)")]
        public decimal? RebatePerto { get; set; }
        [Column("REBATE - Cobra", TypeName = "decimal(16, 2)")]
        public decimal? RebateCobra { get; set; }
        [Required]
        [Column("Analise REBATE")]
        [StringLength(9)]
        public string AnaliseRebate { get; set; }
        [Column("REDUTOR - Perto", TypeName = "numeric(16, 2)")]
        public decimal? RedutorPerto { get; set; }
        [Column("REDUTOR - Cobra", TypeName = "decimal(16, 2)")]
        public decimal? RedutorCobra { get; set; }
        [Required]
        [Column("Analise REDUTOR")]
        [StringLength(9)]
        public string AnaliseRedutor { get; set; }
        [Required]
        [Column("Analise SAT")]
        [StringLength(34)]
        public string AnaliseSat { get; set; }
    }
}
