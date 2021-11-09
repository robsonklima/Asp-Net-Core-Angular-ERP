using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalRefObs
    {
        [Key]
        public int CodInstalRefObs { get; set; }
        public string NomeRefObs { get; set; }
        public byte IndAtivo { get; set; }
    }
}