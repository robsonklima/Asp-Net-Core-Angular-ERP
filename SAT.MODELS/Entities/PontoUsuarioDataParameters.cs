
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioDataParameters : QueryStringParameters
    {
        public string CodUsuario { get; set; }
        public int? CodPontoPeriodo { get; set; }
    }
}
