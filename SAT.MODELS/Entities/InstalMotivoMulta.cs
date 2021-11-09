using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalMotivoMulta
    {
        [Key]
        public int CodInstalMotivoMulta { get; set; }
        public string NomeMotivoMulta { get; set; }
        public string DescMotivoMulta { get; set; }
        public byte IndAtivo { get; set; }        
    }
}