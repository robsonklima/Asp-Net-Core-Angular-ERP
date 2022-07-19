using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class TicketParameters : QueryStringParameters {
        public string CodUsuario { get; set; }
        public int? CodModulo { get; set; }
        public int? CodStatus { get; set; }
        public int? CodPrioridade { get; set; }
        public int? CodClassificacao { get; set; }
    }
}