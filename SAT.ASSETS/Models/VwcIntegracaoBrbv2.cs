using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcIntegracaoBrbv2
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolicitacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        public int? CodTecnico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        public int CodStatusServico { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        public int CodTipoIntervencao { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        public byte? IndLiberacaoFechaduraCofre { get; set; }
        [Column("numBanco")]
        [StringLength(3)]
        public string NumBanco { get; set; }
        public string ObservacaoCliente { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        public int TempoAgendamento { get; set; }
        [StringLength(50)]
        public string NomeDefeito { get; set; }
        [Column("TME")]
        public int? Tme { get; set; }
        [Column("TMR", TypeName = "datetime")]
        public DateTime? Tmr { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InicioAtendimento { get; set; }
        [StringLength(50)]
        public string NomeTipoIntervencao { get; set; }
        [Column("CodigoENomeAutorizada")]
        public string CodigoEnomeAutorizada { get; set; }
        public int? IdAgendamento { get; set; }
    }
}
