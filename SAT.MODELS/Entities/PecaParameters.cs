using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class PecaParameters : QueryStringParameters
    {
        public string CodPeca { get; set; }
        public string CodMagnus { get; set; }
        public PecaIncludeEnum Include { get; set; }
        public PecaFilterEnum FilterType { get; set; }
    }
}
