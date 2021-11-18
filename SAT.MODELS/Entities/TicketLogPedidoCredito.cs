using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TicketLogPedidoCredito
    {
        [Key]
        public int CodTicketLogPedidoCredito { get; set; }
        public int? CodDespesaPeriodoTecnico { get; set; }
        public double? Valor { get; set; }
        public string NumeroCartao { get; set; }
        public int IndProcessado { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
        public string Observacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}