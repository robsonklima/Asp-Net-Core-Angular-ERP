using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class OSPrazoAtendimento
    {
        [Key]
        public int CodOSPrazoAtendimento { get; set; }
        public int CodOS { get; set; }
        public DateTime? DataHoraLimiteAtendimento { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}
