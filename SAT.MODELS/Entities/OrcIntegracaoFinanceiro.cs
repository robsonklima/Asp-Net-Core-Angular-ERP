using System;

namespace SAT.MODELS.Entities {
    public class OrcIntegracaoFinanceiro
    {
        public int CodOrcIntegracaoFinanceiro { get; set; }
        public int CodOrc { get; set; }
        public int TipoFaturamento { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}