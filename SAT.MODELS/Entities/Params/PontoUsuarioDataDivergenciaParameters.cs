using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PontoUsuarioDataDivergenciaParameters : QueryStringParameters
    {
        public int? CodPontoUsuarioData { get; set; }
        public int? DivergenciaValidada { get; set; }
    }
}
