using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class InstalacaoMotivoMulta
    {
        public int CodInstalMotivoMulta { get; set; }
        public string NomeMotivoMulta { get; set; }
        public string DescMotivoMulta { get; set; }
        public byte IndAtivo { get; set; }        
    }
}