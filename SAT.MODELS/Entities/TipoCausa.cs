using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TipoCausa
    {
        [Key]
        public int CodTipoCausa { get; set; }
        public string CodETipoCausa { get; set; }
        public string NomeTipoCausa { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
