using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DispBBPercRegiao
    {
        [Key]
        public int CodDispBBPercRegiao { get; set; }
        public int? CodDispBBRegiao { get; set; }
        public int Criticidade { get; set; }
        public decimal Percentual { get; set; }
        public int IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}