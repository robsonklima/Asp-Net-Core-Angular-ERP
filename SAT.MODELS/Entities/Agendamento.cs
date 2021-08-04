using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("AgendamentoOS")]
    public class Agendamento
    {
        [Key]
        public int? CodAgendamento { get; set; }
        public int? CodMotivo { get; set; }
        [ForeignKey("CodMotivo")]
        public MotivoAgendamento MotivoAgendamento { get; set; }
        public int? CodOS { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public string CodUsuarioAgendamento { get; set; }
        public DateTime? DataHoraUsuAgendamento { get; set; }
    }
}
