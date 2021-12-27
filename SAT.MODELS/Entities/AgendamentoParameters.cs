using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class AgendamentoParameters : QueryStringParameters
    {
        public int? CodAgendamento { get; set; }
        public int? CodOS { get; set; }
    }
}
