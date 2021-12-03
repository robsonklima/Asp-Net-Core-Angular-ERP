using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalTipoPleito")]
    public class InstalacaoTipoPleito
    {
        [Key]
        public int CodInstalTipoPleito { get; set; }
        public string NomeTipoPleito { get; set; }
        public string DescTipoPleito { get; set; }
        public byte IndAtivo { get; set; }
        public string IntroTipoPleito { get; set; }
    }
}