using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaProtocoloParameters : QueryStringParameters
    {
        public string CodTecnicos { get; set; }
        public int? CodFilial { get; set; }
    }
}
