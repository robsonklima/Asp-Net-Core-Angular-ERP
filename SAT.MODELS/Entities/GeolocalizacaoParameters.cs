using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class GeolocalizacaoParameters : QueryStringParameters
    {
        public string EnderecoCEP { get; set; }
        public string LatitudeOrigem { get; set; }
        public string LongitudeOrigem { get; set; }
        public string LatitudeDestino { get; set; }
        public string LongitudeDestino { get; set; }
        public string Pais { get; set; }
        public GeolocalizacaoServiceEnum GeolocalizacaoServiceEnum { get; set; }
    }
}