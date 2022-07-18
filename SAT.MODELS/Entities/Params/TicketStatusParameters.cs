using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class TicketStatusParameters : QueryStringParameters {
        public int codStatus { get; set; }
    }
}