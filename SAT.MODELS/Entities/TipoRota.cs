using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TipoRota
    {
        [Key]
        public int CodTipoRota { get; set; }
        public string NomeTipoRota { get; set; }
    }
}
