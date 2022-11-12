using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class TicketLogTransacaoParameters : QueryStringParameters
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Responsavel { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string NumeroCartao { get; set; }
    }
}
