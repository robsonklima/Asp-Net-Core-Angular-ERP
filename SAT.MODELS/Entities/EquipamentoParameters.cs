using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class EquipamentoParameters : QueryStringParameters
    {
        public int? CodEquip { get; set; }
        public string CodGrupo { get; set; }
        public string CodTipo { get; set; }
    }
}
