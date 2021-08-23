using System;

namespace SAT.MODELS.Entities
{
    public class ContratoSLA
    {
        public int CodContrato { get; set; }
        public int CodSLA { get; set; }
        public byte? IndAgendamento { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
