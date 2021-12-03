using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalRefObs")]
    public class InstalacaoRefObs
    {
        [Key]
        public int CodInstalRefObs { get; set; }
        public string NomeRefObs { get; set; }
        public byte IndAtivo { get; set; }
    }
}