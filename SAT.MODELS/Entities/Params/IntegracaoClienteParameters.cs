using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class IntegracaoClienteParameters: QueryStringParameters
    {
        public string NumOscliente { get; set; }
        public string Chave { get; set; }
    }
}