using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class DboVwcExportacaoChamadosTodosDetalhe
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("DataHoraAberturaOS")]
        [StringLength(4000)]
        public string DataHoraAberturaOs { get; set; }
        [StringLength(4000)]
        public string DataHoraFechamento { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("DescNumSerieNI")]
        [StringLength(20)]
        public string DescNumSerieNi { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        [StringLength(50)]
        public string NomeFantasiaCliente { get; set; }
        [StringLength(3)]
        public string NumBanco { get; set; }
        [StringLength(6)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Column("DistanciaKmPAT_Res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmPatRes { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolicitacao { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        public string Observ { get; set; }
        public byte? IndCancelado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCancelamento { get; set; }
        [StringLength(1000)]
        public string MotivoCancelamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimGarantia { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Required]
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [Column("SEMAT")]
        [StringLength(3)]
        public string Semat { get; set; }
        [StringLength(3)]
        public string PontoEstrategico { get; set; }
        [StringLength(3)]
        public string Rhorario { get; set; }
        [Column("RAcesso")]
        [StringLength(3)]
        public string Racesso { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Required]
        [Column("Horas_para_fechamento")]
        [StringLength(7)]
        public string HorasParaFechamento { get; set; }
        [Column("Dias_para_fechamento")]
        public int? DiasParaFechamento { get; set; }
        [Column("Dia_Abertura")]
        public string DiaAbertura { get; set; }
        [Column("Mes_Abertura")]
        public string MesAbertura { get; set; }
        [Column("Ano_Abertura")]
        public int? AnoAbertura { get; set; }
        public int? NumReincidencia { get; set; }
    }
}
