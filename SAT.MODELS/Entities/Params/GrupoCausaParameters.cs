using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class GrupoCausaParameters : QueryStringParameters
    {
        public int? CodGrupoCausa { get; set; }
        public int? IndAtivo { get; set; }
        public string CodEGrupoCausa { get; set; }
    }
}
