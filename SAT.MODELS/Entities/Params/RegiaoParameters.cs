using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class RegiaoParameters : QueryStringParameters
    {
        public int? CodRegiao { get; set; }
        public int? IndAtivo { get; set; }
        public string NomeRegiao { get; set; }
    }
}
