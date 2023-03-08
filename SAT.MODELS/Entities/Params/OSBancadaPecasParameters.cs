using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OSBancadaPecasParameters : QueryStringParameters
    {
        public string CodOsbancadas { get; set; }
        public string CodPecaRe5114s { get; set; }
        public int? CodOsbancada { get; set; }
        public int? CodPecaRe5114 { get; set; }

    }
}
