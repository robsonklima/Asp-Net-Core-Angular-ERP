using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ContratoEquipamentoParameters : QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodEquip { get; set; }
        public string CodContratos { get; set; }

    }
}
