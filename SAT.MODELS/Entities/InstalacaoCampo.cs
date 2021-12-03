using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalCampo") ]
    public class InstalacaoCampo
    {
        [Key]
        public int CodInstalCampo { get; set; }
        public int? NumInstalCampo { get; set; }
        public string NomeInstalCampo { get; set; }
    }
}