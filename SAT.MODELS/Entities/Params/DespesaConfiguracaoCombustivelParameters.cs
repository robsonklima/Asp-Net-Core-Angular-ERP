using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaConfiguracaoCombustivelParameters : QueryStringParameters
    {
        public int? CodFilial { get; set; }
        public int? CodUF { get; set; }
    }
}