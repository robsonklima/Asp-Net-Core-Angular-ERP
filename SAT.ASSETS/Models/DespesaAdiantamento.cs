using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaAdiantamento")]
    public partial class DespesaAdiantamento
    {
        public DespesaAdiantamento()
        {
            DespesaAdiantamentoPeriodos = new HashSet<DespesaAdiantamentoPeriodo>();
        }

        [Key]
        public int CodDespesaAdiantamento { get; set; }
        public int CodTecnico { get; set; }
        public int CodDespesaAdiantamentoTipo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAdiantamento { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorAdiantamento { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodDespesaAdiantamentoTipo))]
        [InverseProperty(nameof(DespesaAdiantamentoTipo.DespesaAdiantamentos))]
        public virtual DespesaAdiantamentoTipo CodDespesaAdiantamentoTipoNavigation { get; set; }
        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.DespesaAdiantamentos))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
        [InverseProperty(nameof(DespesaAdiantamentoPeriodo.CodDespesaAdiantamentoNavigation))]
        public virtual ICollection<DespesaAdiantamentoPeriodo> DespesaAdiantamentoPeriodos { get; set; }
    }
}
