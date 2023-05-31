using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Despesa")]
    public partial class Despesa
    {
        public Despesa()
        {
            DespesaItems = new HashSet<DespesaItem>();
            DespesaTentativaKms = new HashSet<DespesaTentativaKm>();
        }

        [Key]
        public int CodDespesa { get; set; }
        public int CodDespesaPeriodo { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        public int CodTecnico { get; set; }
        public int? CodFilial { get; set; }
        [Required]
        [StringLength(10)]
        public string CentroCusto { get; set; }
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

        [ForeignKey(nameof(CodDespesaPeriodo))]
        [InverseProperty(nameof(DespesaPeriodo.Despesas))]
        public virtual DespesaPeriodo CodDespesaPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.Despesas))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodRat))]
        [InverseProperty(nameof(Rat.Despesas))]
        public virtual Rat CodRatNavigation { get; set; }
        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.Despesas))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
        [InverseProperty(nameof(DespesaItem.CodDespesaNavigation))]
        public virtual ICollection<DespesaItem> DespesaItems { get; set; }
        [InverseProperty(nameof(DespesaTentativaKm.CodDespesaNavigation))]
        public virtual ICollection<DespesaTentativaKm> DespesaTentativaKms { get; set; }
    }
}
