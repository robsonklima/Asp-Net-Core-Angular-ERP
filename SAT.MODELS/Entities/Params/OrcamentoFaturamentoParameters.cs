using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OrcamentoFaturamentoParameters : QueryStringParameters
    {
        public int? CodOrc { get; set; }
        public double? NumNF { get; set; }
        public string DescricaoNotaFiscal { get; set; }
        public string DataEmissaoNF { get; set; }
        public int? IndFaturado { get; set; }
    }
}