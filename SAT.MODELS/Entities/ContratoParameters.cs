using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class ContratoParameters : QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public string CodCliente { get; set; }
    }
}
