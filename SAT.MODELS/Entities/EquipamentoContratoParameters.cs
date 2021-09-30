using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class EquipamentoContratoParameters : QueryStringParameters
    {
        public int? CodEquipContrato { get; set; }
        public int? CodPosto { get; set; }
        public string CodFiliais { get; set; }
        public string CodEquipamentos { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodFilial { get; set; }
    }
}
