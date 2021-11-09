using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class RelatorioAtendimentoParameters : QueryStringParameters
    {
        public int? CodRAT { get; set; }
        public int? CodOS { get; set; }
    }
}
