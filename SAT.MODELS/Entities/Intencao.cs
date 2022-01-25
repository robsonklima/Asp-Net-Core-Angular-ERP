using System;

namespace SAT.MODELS.Entities
{
    public class Intencao
    {
        public int CodIntencao { get; set; }
        public int? CodOS { get; set; }
        public int? CodTecnico { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public byte? IndAtivo { get; set; }
        public double Distancia { get; set; }
        public double Tempo { get; set; }
    }
}
