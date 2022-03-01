using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class TecnicoOrdemServicoParameters : QueryStringParameters
    {
        public string CodigosStatusServico { get; set; }
        public int? CodTecnico { get; set; }
        public int? IndAtivo { get; set; }
        public int? IndFerias { get; set; }
        public int? CodFilial { get; set; }
    }
}
