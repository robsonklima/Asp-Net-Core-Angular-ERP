using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoPagtoParameters: QueryStringParameters
    {
        public int? CodInstalPagto { get; set; }
        public int? CodContrato { get; set; }
    }
}