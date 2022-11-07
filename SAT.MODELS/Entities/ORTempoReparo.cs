using System;

namespace SAT.MODELS.Entities {
    public class ORTempoReparo
    {
        public int CodORTempoReparo { get; set; }
        public int CodORItem { get; set; }
        public string CodTecnico { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}