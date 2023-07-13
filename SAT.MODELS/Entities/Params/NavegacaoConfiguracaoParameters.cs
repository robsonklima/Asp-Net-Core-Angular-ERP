using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class NavegacaoConfiguracaoParameters : QueryStringParameters
    {
        public int? CodNavegacao { get; set; }
        public int? CodSetor { get; set; }
        public int? CodPerfil { get; set; }

    }
}
