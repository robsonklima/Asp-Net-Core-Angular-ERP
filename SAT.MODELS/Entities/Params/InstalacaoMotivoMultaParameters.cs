using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoMotivoMultaParameters: QueryStringParameters
    {
        public int? CodInstalMotivoMulta { get; set; }
        public string NomeMotivoMulta { get; set; }
        public string DescMotivoMulta { get; set; }
        public byte? IndAtivo { get; set; }    
    }
}