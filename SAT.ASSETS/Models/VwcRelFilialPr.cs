using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelFilialPr
    {
        [Column("OS")]
        public int Os { get; set; }
        [StringLength(30)]
        public string Contrato { get; set; }
        [StringLength(50)]
        public string Regiao { get; set; }
        [StringLength(50)]
        public string Autorizada { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(50)]
        public string Intervencao { get; set; }
        [StringLength(50)]
        public string Cliente { get; set; }
        [StringLength(3)]
        public string NumBanco { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
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
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [StringLength(50)]
        public string StatusServico { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
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
    }
}
