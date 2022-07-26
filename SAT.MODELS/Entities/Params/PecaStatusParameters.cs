using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class PecaStatusParameters : QueryStringParameters
    {
        public string CodPecaStatus { get; set; }
        public PecaIncludeEnum Include { get; set; }
        public PecaFilterEnum FilterType { get; set; }
    }
}
