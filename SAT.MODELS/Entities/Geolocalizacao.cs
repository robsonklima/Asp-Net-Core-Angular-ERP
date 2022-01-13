namespace SAT.MODELS.Entities
{
    public class Geolocalizacao
    {
        public string EnderecoCEP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Endereco { get; set; }
        public string Pais { get; set; }
        public string Numero { get; set; }
        public int? Duracao { get; set; }
        public double? Distancia { get; set; }
    }
}