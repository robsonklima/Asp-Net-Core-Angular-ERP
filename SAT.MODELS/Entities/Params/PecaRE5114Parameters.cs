using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class PecaRE5114Parameters : QueryStringParameters
    {
        public string CodPecas { get; set; }
        public string NumRe5114 { get; set; }
        public int? CodOsbancada { get; set; }
    }
}
