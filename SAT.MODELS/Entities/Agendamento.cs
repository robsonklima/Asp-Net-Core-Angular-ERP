using System;

namespace SAT.MODELS.Entities
{
    public class Agendamento
    {
        public int? CodAgendamento { get; set; }
        public int? CodMotivo { get; set; }
        public MotivoAgendamento MotivoAgendamento { get; set; }
        public int? CodOS { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public string CodUsuarioAgendamento { get; set; }
        public DateTime? DataHoraUsuAgendamento { get; set; }
    }
}
