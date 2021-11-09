using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamento
    {
        [Key]
        public int CodDespesaAdiantamento { get; set; }
        public int CodTecnico { get; set; }
        [ForeignKey("CodTecnico")]
        public Tecnico Tecnico { get; set; }
        public int CodDespesaAdiantamentoTipo { get; set; }
        [ForeignKey("CodDespesaAdiantamentoTipo")]
        public DespesaAdiantamentoTipo DespesaAdiantamentoTipo { get; set; }
        public DateTime DataAdiantamento { get; set; }
        public decimal ValorAdiantamento { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}