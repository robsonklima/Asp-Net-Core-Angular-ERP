using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DispBBRegiaoFilial
    {
        [Key]
        public int CodDispBBRegiaoFilial { get; set; }
        public string CodDispBBRegiao { get; set; }
        public int? CodFilial { get; set; }
        public int? Criticidade { get; set; }
    }
}