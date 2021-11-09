using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoPeriodo
    {
        [Key]
        public int CodDespesaAdiantamentoPeriodo { get; set; }
        public int CodDespesaAdiantamento { get; set; }
        [ForeignKey("CodDespesaAdiantamento")]
        public DespesaAdiantamento DespesaAdiantamento { get; set; }
        public int CodDespesaPeriodo { get; set; }
        [ForeignKey("CodDespesaPeriodo")]
        public DespesaPeriodo DespesaPeriodo { get; set; }
        public decimal ValorAdiantamentoUtilizado { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}