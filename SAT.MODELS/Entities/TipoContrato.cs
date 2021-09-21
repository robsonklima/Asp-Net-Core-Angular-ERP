using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TipoContrato

    {
        [Key]
        public int CodTipoContrato { get; set; }
        public string NomeTipoContrato { get; set; }
    }
}
