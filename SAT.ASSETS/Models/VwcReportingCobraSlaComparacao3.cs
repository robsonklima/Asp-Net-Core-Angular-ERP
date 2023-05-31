﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingCobraSlaComparacao3
    {
        [Column("OS")]
        public int Os { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(50)]
        public string Intervencao { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(3)]
        public string Garantia { get; set; }
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
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("SLACobra")]
        [StringLength(10)]
        public string Slacobra { get; set; }
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
        [Column("DataFimSLA_SemAgend")]
        [StringLength(20)]
        public string DataFimSlaSemAgend { get; set; }
        [Column("DataFimSLA_SemCofre")]
        [StringLength(20)]
        public string DataFimSlaSemCofre { get; set; }
        [Column("DataFimSLA_SemCopa")]
        [StringLength(20)]
        public string DataFimSlaSemCopa { get; set; }
        [Column("KM", TypeName = "decimal(10, 2)")]
        public decimal? Km { get; set; }
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
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
        public int IndQuarentena { get; set; }
    }
}
