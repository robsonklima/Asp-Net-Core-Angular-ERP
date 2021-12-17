using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("DispBBRegiao")]
    public partial class DispBBRegiao
    {
        [Key]
        public int CodDispBBRegiao { get; set; }
        public string Nome { get; set; }
        public int IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DispBBPercRegiao DispBBPercRegiao { get; set; }
    }
}
