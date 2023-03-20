using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class EquipamentoModuloParameters : QueryStringParameters
    {
        public int? IndAtivo { get; set; }
        public int? CodEquip { get; set; }
        public string CodECausa { get; set; }
        public string CodTipoEquips { get; set; }
        public string CodEquips { get; set; }

    }
}
