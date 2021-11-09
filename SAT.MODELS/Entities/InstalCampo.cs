using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalCampo
    {
        [Key]
        public int CodInstalCampo { get; set; }
        public int? NumInstalCampo { get; set; }
        public string NomeInstalCampo { get; set; }
    }
}