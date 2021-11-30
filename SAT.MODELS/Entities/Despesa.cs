using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Despesa
    {
        [Key]
        public int CodDespesa { get; set; }
        public int CodDespesaPeriodo { get; set; }
        public int? CodRAT { get; set; }
        [ForeignKey("CodRAT")]
        public RelatorioAtendimento RelatorioAtendimento { get; set; }
        public int CodTecnico { get; set; }
        [ForeignKey("CodDespesa")]
        public List<DespesaItem> DespesaItens { get; set; }
        public int? CodFilial { get; set; }
        [ForeignKey("CodFilial")]
        public Filial Filial { get; set; }
        public string CentroCusto { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}