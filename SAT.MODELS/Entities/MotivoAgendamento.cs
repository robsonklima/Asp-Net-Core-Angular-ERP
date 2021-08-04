using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class MotivoAgendamento
    {
        [Key]
        public int? CodMotivo { get; set; }
        public string DescricaoMotivo { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndServico { get; set; }
        public byte? IndAgendamentoBB { get; set; }
    }
}
