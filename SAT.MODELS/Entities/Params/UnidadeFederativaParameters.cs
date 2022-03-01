using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class UnidadeFederativaParameters : QueryStringParameters
    {
        public int? CodUF { get; set; }

        public int? CodPais { get; set; }	
    }
}
