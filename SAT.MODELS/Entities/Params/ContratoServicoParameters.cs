using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ContratoServicoParameters : QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodContratoServico{ get; set; }
        public string CodContratos{ get; set; }
        public int? CodTipoEquip{ get; set; }
        public int? CodGrupoEquip{ get; set; }
        public int? CodEquip{ get; set; }
        public int? CodServico{ get; set; }
       public int? CodSLA{ get; set; }

    }
}
