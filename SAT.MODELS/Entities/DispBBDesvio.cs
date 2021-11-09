using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DispBBDesvio
    {
        [Key]
        public int CodDispBBDesvio { get; set; }
        public decimal ValInicial { get; set; }
        public decimal ValFinal { get; set; }
        public decimal Percentual { get; set; }
        public int IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
    }
}