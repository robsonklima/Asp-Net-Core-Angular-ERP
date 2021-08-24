using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class CidadeParameters : QueryStringParameters
    {
        public int? CodCidade { get; set; }
        
        public int? IndAtivo { get; set; }

        public int? CodUF { get; set; }
    }
}
