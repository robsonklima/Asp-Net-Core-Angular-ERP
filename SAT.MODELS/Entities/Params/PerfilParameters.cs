using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PerfilParameters : QueryStringParameters
    {
        public int? CodPerfil { get; set; }
        public int? IndAtivo { get; set; }
    }
}
