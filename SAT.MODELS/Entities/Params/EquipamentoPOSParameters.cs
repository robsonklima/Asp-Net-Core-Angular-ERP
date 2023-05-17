using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class EquipamentoPOSParameters : QueryStringParameters
    {
        public string NumSerie { get; set; }
        public int? CodEquip { get; set; }
    }
}
