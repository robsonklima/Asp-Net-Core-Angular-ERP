using SAT.MODELS.Entities;
using System;

namespace SAT.MODELS.ViewModels
{
    public class DespesaPeriodoTecnicoViewModel
    {
        public int CodTecnico { get; set; }
        public int CodDespesaPeriodo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal TotalDespesa { get; set; }
        public decimal TotalAdiantamento { get; set; }
        public decimal RestituirAEmpresa { get; set; }
        public decimal GastosExcedentes { get; set; }
        public DespesaPeriodoTecnicoStatus Status { get; set; }
        public bool IndAtivo { get; set; }
    }
}