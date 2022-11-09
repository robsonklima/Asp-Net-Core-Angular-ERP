using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class TicketAtendimentoParameters : QueryStringParameters {
        public string CodUsuarioCad { get; set; }
        public int? CodTicket { get; set; }
    }
}