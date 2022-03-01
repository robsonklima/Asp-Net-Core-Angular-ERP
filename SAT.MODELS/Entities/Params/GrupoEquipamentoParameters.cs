using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class GrupoEquipamentoParameters : QueryStringParameters
    {
        public int? CodGrupoEquip { get; set; }
        public int? CodTipoEquip { get; set; }
    }
}
