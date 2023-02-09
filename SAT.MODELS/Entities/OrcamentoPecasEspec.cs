using System;

namespace SAT.MODELS.Entities
{
    public class OrcamentoPecasEspec
    {
        public int CodOrcamentoPecasEspec { get; set; }
        public int? CodPeca { get; set; }
        public int? CodOrcamento { get; set; }
        public int? CodOsbancada { get; set; }
        public decimal? Quantidade { get; set; }
        public int? CodPecaRe5114 { get; set; }
        public byte? IndCobranca { get; set; }
        public decimal? ValorPeca { get; set; }
        public byte? TipoDesconto { get; set; }
        public int? CodBancadaLista { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? PercIpi { get; set; }
        public PecaRE5114 PecaRE5114 { get; set; }
        public Peca Peca { get; set; }
        public OSBancada OSBancada { get; set; }
        public OsBancadaPecasOrcamento OSBancadaPecasOrcamento { get; set; }
    }
}
