using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaConfiguracaoCombustivelParameters : QueryStringParameters
    {
        public int? CodFilial { get; set; }
        public int? CodUf { get; set; }
    }
}