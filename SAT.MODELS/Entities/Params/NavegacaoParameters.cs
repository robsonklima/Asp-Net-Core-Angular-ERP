using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class NavegacaoParameters : QueryStringParameters
    {
        public int? CodNavegacao { get; set; }
        public int? CodNavegacaoPai { get; set; }
    }
}
