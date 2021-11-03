using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class DespesaAdiantamentoPeriodoConsultaTecnicoItem
    {
        public Tecnico Tecnico { get; set; }
        public decimal SaldoAdiantamento { get; set; }
        public bool Liberado { get; set; }
        public bool IndAtivo { get; set; }
    }

    public class DespesaAdiantamentoPeriodoConsultaTecnicoViewModel : Meta
    {
        public List<DespesaAdiantamentoPeriodoConsultaTecnicoItem> Items { get; set; }
    }
}