using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class AutorizadaParameters : QueryStringParameters
    {
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        public int? IndAtivo { get; set; }
    }
}
