using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class AdendoParameters : QueryStringParameters
    {
        public int? CodEquipContrato { get; set; }
    }

    public class AdendoItemParameters : QueryStringParameters
    {
        public int? CodEquipContrato { get; set; }
    }
}
