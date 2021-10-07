using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Geolocalizacao
    {
        [Key]
        public int CodGeolocalizacao { get; set; }
        public string EnderecoCEP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GoogleGeolocationJson { get; set; }
    }
}
