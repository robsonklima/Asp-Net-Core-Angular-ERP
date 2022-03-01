using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class TransportadoraParameters : QueryStringParameters
    {
        public int? CodTransportadora { get; set; }
        public int? indAtivo { get; set; }
    }
}
