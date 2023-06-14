using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class AutorizadaParameters : QueryStringParameters
    {
        public int? CodAutorizada { get; set; }
        public string CodAutorizadas { get; set; }
        public string CodFiliais { get; set; }
        public int? CodFilial { get; set; }
        public int? IndAtivo { get; set; }
        public byte? IndFilialPerto { get; set; }
    }
}
