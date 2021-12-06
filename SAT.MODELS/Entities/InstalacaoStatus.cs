using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalStatus") ]
    public class InstalacaoStatus
    {
        [Key]
        public int CodInstalStatus { get; set; }
        public string NomeInstalStatus { get; set; }
        public byte IndAtivo { get; set; }
    }
}