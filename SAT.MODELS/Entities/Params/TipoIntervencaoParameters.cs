using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class TipoIntervencaoParameters : QueryStringParameters
    {
        public int? CodTipoIntervencao { get; set; }
        public int? IndAtivo { get; set; }
    }
}
