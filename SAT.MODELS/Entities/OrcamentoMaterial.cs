using System;

namespace SAT.MODELS.Entities
{
    public class OrcamentoMaterial
    {
        public int CodOrcMaterial { get; set; }
        public int? CodOrc { get; set; }
        public string CodigoMagnus { get; set; }
        public int? CodigoPeca { get; set; }
        public string Descricao { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorTotal { get; set; }
        public int? Quantidade { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string UsuarioCadastro { get; set; }
        public decimal? ValorIpi { get; set; }
        public decimal? ValorUnitarioFinanceiro { get; set; }
        public int? SeqItemPedido { get; set; }
        public virtual Peca Peca { get; set; }
    }
}