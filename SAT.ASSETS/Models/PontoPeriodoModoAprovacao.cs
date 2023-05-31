using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoPeriodoModoAprovacao")]
    public partial class PontoPeriodoModoAprovacao
    {
        public PontoPeriodoModoAprovacao()
        {
            PontoPeriodos = new HashSet<PontoPeriodo>();
        }

        [Key]
        public int CodPontoPeriodoModoAprovacao { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [InverseProperty(nameof(PontoPeriodo.CodPontoPeriodoModoAprovacaoNavigation))]
        public virtual ICollection<PontoPeriodo> PontoPeriodos { get; set; }
    }
}
