using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalStatus
    {
        [Key]
        public int CodInstalStatus { get; set; }
        public string NomeInstalStatus { get; set; }
        public byte IndAtivo { get; set; }
    }
}