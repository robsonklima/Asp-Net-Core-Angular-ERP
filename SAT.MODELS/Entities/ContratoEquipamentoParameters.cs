using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class ContratoEquipamentoParameters : QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodEquip { get; set; }
    }
}
