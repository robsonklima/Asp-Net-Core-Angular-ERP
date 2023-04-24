using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class CidadeParameters : QueryStringParameters
    {
        public int? CodCidade { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodUF { get; set; }
        public string CodFiliais { get; set; }
        public string CodUFs { get; set; }
        public string NomeCidade { get; set; }
        public string SiglaUF { get; set; }
    }
}