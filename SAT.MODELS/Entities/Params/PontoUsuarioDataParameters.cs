
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PontoUsuarioDataParameters : QueryStringParameters
    {
        public string CodUsuario { get; set; }
        public int? CodPontoPeriodo { get; set; }
    }
}
