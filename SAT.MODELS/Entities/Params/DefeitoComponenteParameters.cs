using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DefeitoComponenteParameters : QueryStringParameters
    {
        public string CodECausa { get; set; }
        public string CodDefeitos { get; set; }
        public string CodCausas { get; set; }
    }
}
