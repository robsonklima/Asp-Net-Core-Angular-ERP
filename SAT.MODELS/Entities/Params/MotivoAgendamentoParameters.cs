using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class MotivoAgendamentoParameters : QueryStringParameters
    {
        public int? CodMotivo{ get; set; }
        public int? IndAtivo { get; set; }
    }
}
