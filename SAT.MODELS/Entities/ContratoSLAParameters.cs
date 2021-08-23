using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class ContratoSLAParameters : QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodSLA{ get; set; }
    }
}
