using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OrdemServicoSTNParameters : QueryStringParameters
    {
        public string CodClientes { get; set; }
        public int? CodOS { get; set; }
        public int? CodAtendimento { get; set; }
        public string CodFiliais { get; set; }
        public string CodEquips { get; set; }
        public string CodOrigemChamadoSTNs { get; set; }
        public string CodUsuarios { get; set; }
    }
}
