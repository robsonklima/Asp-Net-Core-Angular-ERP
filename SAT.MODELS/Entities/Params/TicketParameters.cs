using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class TicketParameters : QueryStringParameters {
        public string CodUsuarioCad { get; set; }
        public int? CodModulo { get; set; }
        public string CodStatus { get; set; }
        public int? CodPrioridade { get; set; }
        public int? CodClassificacao { get; set; }
        public DateTime? DataHoraCadInicio { get; set; }
        public DateTime? DataHoraCadFim { get; set; }
    }
}