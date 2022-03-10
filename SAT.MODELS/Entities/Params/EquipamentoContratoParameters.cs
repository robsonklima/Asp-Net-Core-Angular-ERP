using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class EquipamentoContratoParameters : QueryStringParameters
    {
        public int? CodEquipContrato { get; set; }
        public int? CodPosto { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodCliente { get; set; }
        public int? CodFilial { get; set; }
        public string CodFiliais { get; set; }
        public int? CodContrato { get; set; }
        public string CodEquipamentos { get; set; }
        public string NumSerie { get; set; }

    }
}
