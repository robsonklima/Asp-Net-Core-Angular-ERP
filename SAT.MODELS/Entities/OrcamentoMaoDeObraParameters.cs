using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class OrcamentoMaoDeObraParameters : QueryStringParameters
    {
        public int CodOrcMaoObra { get; set; }
        public int? CodOrc { get; set; }
    }
}