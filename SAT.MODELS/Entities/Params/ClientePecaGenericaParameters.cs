using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class ClientePecaGenericaParameters : QueryStringParameters
    {
        public int? CodClientePecaGenerica { get; set; }
        public string CodMagnus { get; set; }
        public PecaIncludeEnum Include { get; set; }
        public PecaFilterEnum FilterType { get; set; }
    }
}
