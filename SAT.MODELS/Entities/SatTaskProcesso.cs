using System;

namespace SAT.MODELS.Entities
{
    public class SatTaskProcesso
    {
        public int? CodSatTaskProcesso { get; set; }
        public int CodSatTaskTipo { get; set; }
        public int CodOS { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
        public byte? IndProcessado { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public String Descricao { get; set; }
        public SatTaskTipo Tipo { get; set; }
    }
}
