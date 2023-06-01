using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaAdiantamentoPeriodo")]
    public partial class DespesaAdiantamentoPeriodo
    {
        [Key]
        public int CodDespesaAdiantamentoPeriodo { get; set; }
        public int CodDespesaAdiantamento { get; set; }
        public int CodDespesaPeriodo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorAdiantamentoUtilizado { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodDespesaAdiantamento))]
        [InverseProperty(nameof(DespesaAdiantamento.DespesaAdiantamentoPeriodos))]
        public virtual DespesaAdiantamento CodDespesaAdiantamentoNavigation { get; set; }
        [ForeignKey(nameof(CodDespesaPeriodo))]
        [InverseProperty(nameof(DespesaPeriodo.DespesaAdiantamentoPeriodos))]
        public virtual DespesaPeriodo CodDespesaPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.DespesaAdiantamentoPeriodos))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
    }
}
