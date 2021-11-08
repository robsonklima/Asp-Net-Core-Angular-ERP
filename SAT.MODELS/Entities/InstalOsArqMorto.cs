using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalOsArqMorto
    {
        [Key]
        public int CodInstalacao { get; set; }
        public int CodOS { get; set; }
        public int? CodRAT { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }        
    }
}