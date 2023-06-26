using System;

namespace SAT.MODELS.Entities
{
    public class SatTask
    {
        public int? CodSatTask { get; set; }
        public string Status { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
        public int CodSatTaskTipo { get; set; }
        public SatTaskTipo Tipo { get; set; }
    }
}
