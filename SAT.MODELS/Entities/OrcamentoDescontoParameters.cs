using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class OrcamentoDescontoParameters : QueryStringParameters
    {
        public int CodOrcDesconto { get; set; }
        public int? CodOrc { get; set; }
        public string NomeTipo { get; set; }
    }
}