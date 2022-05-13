using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class CheckinCheckoutParameters : QueryStringParameters
    {
        public int? CodOS { get; set; }
        public int? CodRAT { get; set; }
        public string CodUsuarioTecnico { get; set; }
        
    }
}
