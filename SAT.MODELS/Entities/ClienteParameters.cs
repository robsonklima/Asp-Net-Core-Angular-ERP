using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class ClienteParameters : QueryStringParameters
    {
        public int? CodCliente { get; set; }
        public int? IndAtivo { get; set; }
    }
}
