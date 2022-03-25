using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class EquipamentoModuloParameters : QueryStringParameters
    {
        public int? CodEquip { get; set; }
        public int? IndAtivo { get; set; }
        public string CodECausa { get; set; }
    }
}
