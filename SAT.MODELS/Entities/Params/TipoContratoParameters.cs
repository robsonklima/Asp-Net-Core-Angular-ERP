using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class TipoContratoParameters : QueryStringParameters
    {
        public int? CodTipoContrato { get; set; }
        public string NomeTipoContrato { get; set; }
    }
}
