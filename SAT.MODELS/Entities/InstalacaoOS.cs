using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalOS")]
    public class InstalacaoOS
    {
        [Key]
        public int CodInstalacao { get; set; }
        public int CodOS { get; set; }
        public int? CodRAT { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}