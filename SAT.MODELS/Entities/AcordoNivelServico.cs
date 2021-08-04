using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("sla_new")]
    public class AcordoNivelServico
    {
        [Key]
        public int CodSLA { get; set; }
        public string NomeSLA { get; set; }
        public string DescSLA { get; set; }
    }
}
