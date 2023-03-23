using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class RelatorioAtendimentoDetalhePecaStatusParameters : QueryStringParameters {
        public int? CodRATDetalhesPecasStatus { get; set; }
        public int? CodRATDetalhesPecas { get; set; }
        public string CodRATDetalhesPecasIN { get; set; }
    }
}