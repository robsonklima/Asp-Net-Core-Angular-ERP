using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaPeriodoParameters : QueryStringParameters
    {
        public int? IndAtivo { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public DateTime? FimPeriodo { get; set; }
    }
}