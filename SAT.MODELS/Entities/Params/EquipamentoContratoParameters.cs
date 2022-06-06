using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class EquipamentoContratoParameters : QueryStringParameters
    {
        public int? CodEquipContrato { get; set; }
        public int? CodPosto { get; set; }
        public int? IndAtivo { get; set; }
        public string CodClientes { get; set; }
        public int? CodFilial { get; set; }
        public int? CodContrato { get; set; }
        public string CodFiliais { get; set; }
        public string CodPostos { get; set; }
        public string CodAutorizadas{ get; set; }
        public string CodRegioes { get; set; }
        public string CodTipoContratos { get; set; }
        public string CodEquips { get; set; }
        public string CodTipoEquips { get; set; }
        public string CodGrupoEquips { get; set; }
        public string CodEquipamentos { get; set; }
        public string NumSerie { get; set; }

    }
}
