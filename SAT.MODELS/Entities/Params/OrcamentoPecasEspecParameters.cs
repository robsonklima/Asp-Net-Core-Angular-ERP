using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OrcamentoPecasEspecParameters : QueryStringParameters
    {
        public string CodOrcamentos { get; set; }
        public string CodOsbancadas { get; set; }
        public string CodPecaRe5114s { get; set; }
        public string CodPecas { get; set; }
        public int? CodOrcamento { get; set; }

    }
}
