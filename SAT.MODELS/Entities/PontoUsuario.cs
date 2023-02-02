using System;

namespace SAT.MODELS.Entities
{
    public class PontoUsuario
    {
        public int CodPontoUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        public PontoPeriodo PontoPeriodo { get; set; }
        public string CodUsuario { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public DateTime DataHoraEnvio { get; set; }
        public byte? IndAprovado { get; set; }
        public byte? IndRevisado { get; set; }
        public byte IndAtivo { get; set; }
        public string Observacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public string CodUsuarioAprov { get; set; }
        public string CodUsuarioManut { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}