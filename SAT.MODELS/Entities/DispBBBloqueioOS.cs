using System;

namespace SAT.MODELS.Entities
{
    public class DispBBBloqueioOS
    {
        public int CodDispBBBloqueioOS { get; set; }
        public int CodOS { get; set; }
        public int IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string TipoBloqueio { get; set; }
    }
}