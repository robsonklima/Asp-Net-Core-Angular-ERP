using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwSarmentoExportacaoChamado
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("NumOSQuarteirizada")]
        [StringLength(20)]
        public string NumOsquarteirizada { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("DescNumSerieNI")]
        [StringLength(20)]
        public string DescNumSerieNi { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [StringLength(50)]
        public string NomeFantasiaCliente { get; set; }
        [StringLength(3)]
        public string NumBanco { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [StringLength(6)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Column("DistanciaKmPAT_Res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmPatRes { get; set; }
        [Column("NomeEquipSNS")]
        [StringLength(50)]
        public string NomeEquipSns { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolicitacao { get; set; }
        [Column("ultimoatendimento", TypeName = "datetime")]
        public DateTime? Ultimoatendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Expr1 { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        public string Observ { get; set; }
        public byte? IndCancelado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCancelamento { get; set; }
        [StringLength(1000)]
        public string MotivoCancelamento { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        [Required]
        [StringLength(1)]
        public string PontoEstrategico { get; set; }
        public byte Rhorario { get; set; }
        [Column("RAcesso")]
        public byte Racesso { get; set; }
        [Column("RAT")]
        [StringLength(20)]
        public string Rat { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InicioAtendimento { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [Column("DataFimSLA", TypeName = "datetime")]
        public DateTime? DataFimSla { get; set; }
        [Column("KM")]
        public int? Km { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Required]
        [Column("SEMAT")]
        [StringLength(3)]
        public string Semat { get; set; }
    }
}
