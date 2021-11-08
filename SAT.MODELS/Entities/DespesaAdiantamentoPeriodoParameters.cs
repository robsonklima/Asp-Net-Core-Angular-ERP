using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoPeriodoParameters : QueryStringParameters
    {
        public int? CodDespesaPeriodo { get; set; }
        public int? CodTecnico { get; set; }
        public int? IndAtivoTecnico { get; set; }
        public int? IndAdiantamentoAtivo { get; set; }
        public int? IndTecnicoLiberado { get; set; }
        public string CodFiliais { get; set; }
    }
}