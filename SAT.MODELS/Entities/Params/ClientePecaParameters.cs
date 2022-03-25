using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class ClientePecaParameters : QueryStringParameters
    {
        public int? CodClientePeca { get; set; }
        public string CodMagnus { get; set; }
        public int? CodContrato { get; set; }
        public PecaIncludeEnum Include { get; set; }
        public PecaFilterEnum FilterType { get; set; }
    }
}
