using System;

namespace SAT.MODELS.Entities
{
    public class OSPrazoAtendimento
    {
        public int CodOSPrazoAtendimento { get; set; }
        public int CodOS { get; set; }
        public DateTime? DataHoraLimiteAtendimento { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}