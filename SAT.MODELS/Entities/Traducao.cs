using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Traducao
    {
        [Key]
        public int CodTraducao { get; set; }
        [ForeignKey("CodLingua")]
        public Lingua Lingua { get; set; }
        public string CodETraducao { get; set; }
        public string NomeTraducao { get; set; }
    }
}
