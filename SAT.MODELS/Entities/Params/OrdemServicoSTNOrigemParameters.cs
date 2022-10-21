using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OrdemServicoSTNOrigemParameters : QueryStringParameters
    {
        public int? CodOrigemChamadoSTN { get; set; }
        public string DescOrigemChamadoSTN { get; set; }
        public int? IndAtivo { get; set; }
    }
}
