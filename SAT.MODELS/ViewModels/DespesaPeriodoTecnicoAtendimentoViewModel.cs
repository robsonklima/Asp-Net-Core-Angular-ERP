using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class DespesaPeriodoTecnicoAtendimentoItem
    {
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

    public class DespesaPeriodoTecnicoAtendimentoViewModel : Meta
    {
        public List<DespesaPeriodoTecnicoAtendimentoItem> Items { get; set; }
    }
}