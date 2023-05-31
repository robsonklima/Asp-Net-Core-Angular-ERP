using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaTipo")]
    public partial class DespesaTipo
    {
        public DespesaTipo()
        {
            DespesaItems = new HashSet<DespesaItem>();
            DespesaTentativaKms = new HashSet<DespesaTentativaKm>();
        }

        [Key]
        public int CodDespesaTipo { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipo { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipoAbreviado { get; set; }
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

        [InverseProperty(nameof(DespesaItem.CodDespesaTipoNavigation))]
        public virtual ICollection<DespesaItem> DespesaItems { get; set; }
        [InverseProperty(nameof(DespesaTentativaKm.CodDespesaTipoNavigation))]
        public virtual ICollection<DespesaTentativaKm> DespesaTentativaKms { get; set; }
    }
}
