using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalTipoParcela")]
    public class InstalacaoTipoParcela
    {
        [Key]
        public int CodInstalTipoParcela { get; set; }
        public string NomeTipoParcela { get; set; }
        public byte IndAtivo { get; set; }
    }
}