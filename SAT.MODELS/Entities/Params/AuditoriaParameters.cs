using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class AuditoriaParameters : QueryStringParameters {
        public int CodAuditoria { get; set; }
        public string CodUsuario { get; set; }
        public int? CodAuditoriaStatus { get; set; }
        public int? CodAuditoriaVeiculo { get; set; }

        
    }
}