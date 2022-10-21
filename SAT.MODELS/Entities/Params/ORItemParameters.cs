using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ORItemParameters : QueryStringParameters
    {
        public int? CodOR { get; set; }
        public string CodTiposOR { get; set; }
        public string CodStatus { get; set; }
        public string CodMagnus { get; set; }
    }
}