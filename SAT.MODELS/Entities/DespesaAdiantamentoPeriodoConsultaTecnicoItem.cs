using SAT.MODELS.Entities;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoPeriodoConsultaTecnicoItem
    {
        public Tecnico Tecnico { get; set; }
        public decimal SaldoAdiantamento { get; set; }
        public bool Liberado { get; set; }
        public bool IndAtivo { get; set; }
    }
}