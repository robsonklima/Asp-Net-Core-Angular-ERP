using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class StatusSLAOSAberta
    {
        [Key]
        public int CodStatusSLAOSAberta { get; set; }
        [ForeignKey("CodOS")]
        public int CodOS { get; set; }
        public virtual OrdemServico OrdemServico { get; set; }
        public int CodCliente { get; set; }
        public DateTime? DataHoraLimiteAtendimento { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
        [NotMapped]
        public string StatusSLAOS { get; set; }
        public int KMDistancia { get; set; }
    }
}
