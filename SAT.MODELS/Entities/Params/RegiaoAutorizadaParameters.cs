using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class RegiaoAutorizadaParameters : QueryStringParameters
    {
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public string CodFiliais { get; set; }
        public string CodAutorizadas { get; set; }
        public string CodCidades { get; set; }
        public int? CodCidade { get; set; }
        public int? IndAtivo { get; set; }
    }
}
