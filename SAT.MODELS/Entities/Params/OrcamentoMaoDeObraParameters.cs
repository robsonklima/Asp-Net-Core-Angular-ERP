using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OrcamentoMaoDeObraParameters : QueryStringParameters
    {
        public int CodOrcMaoObra { get; set; }
        public int? CodOrc { get; set; }
    }
}