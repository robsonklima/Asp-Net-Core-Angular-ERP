using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class CausaParameters : QueryStringParameters
    {
        public int? CodCausa { get; set; }
        public int? IndAtivo { get; set; }
        public int? ApenasModulos { get; set; }
        
    }
}
