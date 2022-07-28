using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class FormaPagamentoParameters : QueryStringParameters
    {
        public int? IndAtivo { get; set; }

        public string CodFormasPagto { get; set; }

    }
}
