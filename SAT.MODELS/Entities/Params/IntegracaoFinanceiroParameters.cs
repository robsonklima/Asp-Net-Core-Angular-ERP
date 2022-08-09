using System;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class IntegracaoFinanceiroParameters
    {
        public int? CodOrc { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public int? CodStatusServico { get; set; }
        public DateTime DataFechamento { get; set; }
        public TipoFaturamentoOrcEnum? TipoFaturamento { get; set; }
    }
}