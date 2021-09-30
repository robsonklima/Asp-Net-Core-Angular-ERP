using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class FilialParameters : QueryStringParameters
    {
        public int? CodFilial { get; set; }
        public string CodFiliais { get; set; }
        public int? IndAtivo { get; set; }
        public string SiglaUF { get; set; }
    }
}
