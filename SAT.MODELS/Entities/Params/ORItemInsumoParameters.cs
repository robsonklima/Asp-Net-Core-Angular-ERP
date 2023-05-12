using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ORItemInsumoParameters : QueryStringParameters
    {
        public int? CodORItem { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodPeca { get; set; }
    }
}