using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class TipoIntervencaoParameters : QueryStringParameters
    {
        public int? CodTipoIntervencao { get; set; }
        public int? IndAtivo { get; set; }
    }
}
