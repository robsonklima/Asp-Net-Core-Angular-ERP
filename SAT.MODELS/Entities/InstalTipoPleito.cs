using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalTipoPleito
    {
        [Key]
        public int CodInstalTipoPleito { get; set; }
        public string NomeTipoPleito { get; set; }
        public string DescTipoPleito { get; set; }
        public byte IndAtivo { get; set; }
        public string IntroTipoPleito { get; set; }
    }
}