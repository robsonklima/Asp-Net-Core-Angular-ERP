using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OsBancadaPecasOrcamentoParameters : QueryStringParameters
    {
        public string CodPecaRe5114s { get; set; }
        public string CodOSBancadas { get; set; }
        public string NumRe5114 { get; set; }
        public int? CodOrcamento { get; set; }
        public string CodClienteBancadas { get; set; }
        public int? IndOrcamentoAprov { get; set; }

    }
}
