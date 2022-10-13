using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Despesa
    {
        public int CodDespesa { get; set; }
        public int CodDespesaPeriodo { get; set; }
        public DespesaPeriodo DespesaPeriodo { get; set; }
        public int? CodRAT { get; set; }
        public RelatorioAtendimento RelatorioAtendimento { get; set; }
        public int CodTecnico { get; set; }
        public List<DespesaItem> DespesaItens { get; set; }
        public int? CodFilial { get; set; }
        public Filial Filial { get; set; }
        public string CentroCusto { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}