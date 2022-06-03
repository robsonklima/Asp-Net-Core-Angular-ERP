using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ContratoParameters : QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodTipoContrato { get; set; }
        public int? CodCliente { get; set; }
        public int? IndAtivo { get; set; }
        public string CodClientes { get; set; }
    }
}
