using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ItemDefeitoParameters : QueryStringParameters
    {
        public int? CodItemDefeito { get; set; }
        public int? CodORItem { get; set; }
        public int? CodDefeito { get; set; }
    }
}
