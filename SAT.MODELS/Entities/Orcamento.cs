using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Orcamento
    {
        public int CodOrc { get; set; }
        public int? CodigoMotivo { get; set; }
        public int? CodigoStatus { get; set; }
        public int? CodigoSla { get; set; }
        public int? CodigoEquipamento { get; set; }
        public int? CodigoCliente { get; set; }
        public int? CodigoPosto { get; set; }
        public int? CodigoFilial { get; set; }
        public int? CodigoContrato { get; set; }
        public byte? IsMaterialEspecifico { get; set; }
        public int? CodigoOrdemServico { get; set; }
        public int? CodigoEquipamentoContrato { get; set; }
        public string DescricaoOutroMotivo { get; set; }
        public string Detalhe { get; set; }
        public string NomeContrato { get; set; }
        public string Numero { get; set; }
        public DateTime? Data { get; set; }
        public decimal? ValorIss { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? ValorTotalDesconto { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string UsuarioCadastro { get; set; }
        public DateTime? DataEnvioAprovacao { get; set; }
        public DateTime? DataAprovacaoCliente { get; set; }
        public string NumPedido { get; set; }
        public string ObsPedido { get; set; }
        public byte? IndFaturamento { get; set; }
        public DateTime? DataHoraFaturamento { get; set; }
        public string CodUsuarioFaturamento { get; set; }
        public byte? IndEnvioCliente { get; set; }
        public DateTime? DataHoraEnvioCliente { get; set; }
        public string CodUsuarioEnvioCliente { get; set; }
        public LocalEnvioNFFaturamentoVinculado LocalEnvioNFFaturamentoVinculado { get; set; }
        public virtual OrdemServico OrdemServico { get; set; }
        [ForeignKey("CodigoFilial")]
        public virtual Filial Filial { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual OrcamentoMotivo OrcamentoMotivo { get; set; }
        public virtual List<OrcamentoMaterial> Materiais { get; set; }
        public virtual List<OrcamentoOutroServico> OutrosServicos { get; set; }
        public virtual OrcamentoMaoDeObra MaoDeObra { get; set; }
        public virtual List<OrcamentoDesconto> Descontos { get; set; }
        public virtual OrcamentoStatus OrcamentoStatus { get; set; }
        public virtual OrcamentoDeslocamento OrcamentoDeslocamento { get; set; }
    }
}