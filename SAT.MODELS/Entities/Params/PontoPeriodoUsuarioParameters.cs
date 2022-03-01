using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PontoPeriodoUsuarioParameters : QueryStringParameters
    {
        public int? CodPontoPeriodo { get; set; }
        public string CodUsuario { get; set; }
    }
}
