using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class GoogleGeolocationParameters : QueryStringParameters
    {
        public string EnderecoCEP { get; set; }
        public string LatitudeOrigem { get; set; }
        public string LongitudeOrigem { get; set; }
        public string LatitudeDestino { get; set; }
        public string LongitudeDestino { get; set; }
    }
}