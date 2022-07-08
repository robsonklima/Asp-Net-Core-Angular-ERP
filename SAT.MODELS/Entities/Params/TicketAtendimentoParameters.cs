using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class TicketAtendimentoParameters : QueryStringParameters {
        public string UsuarioAtend { get; set; }
        public int? CodTicket { get; set; }
    }
}