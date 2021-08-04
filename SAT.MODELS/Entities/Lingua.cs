using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Lingua
    {
        [Key]
        public int CodLingua { get; set; }
        public string NomeLingua { get; set; }
        public string Cultura { get; set; }
    }
}
