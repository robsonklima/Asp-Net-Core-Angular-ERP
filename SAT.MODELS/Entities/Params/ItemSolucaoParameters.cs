using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ItemSolucaoParameters : QueryStringParameters
    {
        public int? CodItemSolucao { get; set; }
        public int? CodORItem { get; set; }
        public int? CodSolucao { get; set; }
    }
}
