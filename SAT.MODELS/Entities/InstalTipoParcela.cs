using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalTipoParcela
    {
        [Key]
        public int CodInstalTipoParcela { get; set; }
        public string NomeTipoParcela { get; set; }
        public byte IndAtivo { get; set; }
    }
}