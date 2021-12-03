using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("RATDetalhes")]
    public class RelatorioAtendimentoDetalhe
    {
        [Key]
        public int CodRATDetalhe { get; set; }
        public int CodTipoCausa { get; set; }
        public int CodGrupoCausa { get; set; }
        public int CodDefeito { get; set; }
        public int CodCausa { get; set; }
        public int? CodOrigemCausa { get; set; }
        public int CodAcao { get; set; }
        public int? CodServico { get; set; }
        public int? IndDefeitoMaquina { get; set; }
        public string CodUsuarioCad { get; set; }
        public int CodOS { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        [ForeignKey("CodRAT")]
        public int CodRAT { get; set; }
        [ForeignKey("CodTipoCausa")]
        public TipoCausa TipoCausa { get; set; }
        [ForeignKey("CodGrupoCausa")]
        public GrupoCausa GrupoCausa { get; set; }
        [ForeignKey("CodDefeito")]
        public Defeito Defeito { get; set; }
        [ForeignKey("CodCausa")]
        public Causa Causa { get; set; }
        [ForeignKey("CodAcao")]
        public Acao Acao { get; set; }
        [ForeignKey("CodServico")]
        public TipoServico TipoServico { get; set; }
        [ForeignKey("CodRATDetalhe")]
        public List<RelatorioAtendimentoDetalhePeca> RelatorioAtendimentoDetalhePecas { get; set; }
    }
}
