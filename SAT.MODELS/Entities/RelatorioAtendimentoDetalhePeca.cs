using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("RATDetalhesPecas")]
    public class RelatorioAtendimentoDetalhePeca
    {
        [Key]
        [Column("CodRATDetalhesPecas")]
        public int CodRATDetalhePeca { get; set; }
        public int CodRATDetalhe { get; set; }
        public int CodPeca { get; set; }
        [ForeignKey("CodPeca")]
        public Peca Peca { get; set; }
        public int QtdePecas { get; set; }
        public decimal? ValPecas { get; set; }
        [Column("A_P")]
        public int? AP { get; set; }
        public DateTime? DatIncPP { get; set; }
        public int? QtdeLib { get; set; }
        public string DescStatus { get; set; }
        public int? CodPecaSubst { get; set; }
        public byte? IndPecaSubst { get; set; }
        public byte? IndCentral { get; set; }
        public byte? IndOK { get; set; }
        public byte? IndNotaFiscal { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodMagnusInconsistente { get; set; }
        public byte? IndPassivelConserto { get; set; }
        public string NotaFiscal { get; set; }
        public int? NfStatus { get; set; }
        public string NumSerie { get; set; }
        public string MotivoSubstituicao { get; set; }
        [ForeignKey("CodRATDetalhesPecas")]
        public List<RelatorioAtendimentoDetalhePecaStatus> RelatorioAtendimentoDetalhePecaStatus { get; set; }
    }
}
