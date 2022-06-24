using System;
using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class TicketLogPedidoCreditoParameters : QueryStringParameters
    {
        public int? CodDespesaPeriodoTecnico { get; set; }
    }
}
