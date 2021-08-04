using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("RATFotoSmartphone")]
    public class Foto
    {
        [Key]
        public int? CodRATFotoSmartphone { get; set; }
        public int CodOS { get; set; }
        public string NumRAT { get; set; }
        public string NomeFoto { get; set; }
        public string Modalidade { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}
