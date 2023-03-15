using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoPagtoInstalParameters: QueryStringParameters
    {
        public int? CodInstalacao { get; set; }
        public int? CodInstalPagto { get; set; }
        public int? CodInstalTipoParcela { get; set; }
        public int? CodInstalMotivoMulta { get; set; }
    }
}