using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ClienteParameters : QueryStringParameters
    {
        public int? CodCliente { get; set; }
        public int? IndAtivo { get; set; }
        public string NomeFantasia { get; set; }
    }
}
