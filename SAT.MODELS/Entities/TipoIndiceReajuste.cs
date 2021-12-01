using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TipoIndiceReajuste
    {
        [Key]
        public int CodTipoIndiceReajuste { get; set; }
        public string NomeTipoIndiceReajuste { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
