using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class PontoPeriodoUsuarioParameters : QueryStringParameters
    {
        public int? CodPontoPeriodo { get; set; }
        public string CodUsuario { get; set; }
    }
}
