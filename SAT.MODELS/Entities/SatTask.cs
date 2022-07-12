using System;

namespace SAT.MODELS.Entities
{
    public class SatTask
    {
        public int CodSatTask { get; set; }
        public int codSatTaskTipo { get; set; }
        public DateTime DataHoraProcessamento { get; set; }
        public SatTaskTipo Tipo { get; set; }
    }
}
