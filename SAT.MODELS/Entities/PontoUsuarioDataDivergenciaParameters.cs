using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataDivergenciaParameters : QueryStringParameters
    {
        public int? CodPontoUsuarioData { get; set; }
        public int? DivergenciaValidada { get; set; }
    }
}
