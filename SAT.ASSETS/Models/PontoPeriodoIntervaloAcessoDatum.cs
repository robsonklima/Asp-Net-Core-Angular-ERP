using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PontoPeriodoIntervaloAcessoDatum
    {
        public PontoPeriodoIntervaloAcessoDatum()
        {
            PontoPeriodos = new HashSet<PontoPeriodo>();
        }

        [Key]
        public int CodPontoPeriodoIntervaloAcessoData { get; set; }
        public int IntervaloDias { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(PontoPeriodo.CodPontoPeriodoIntervaloAcessoDataNavigation))]
        public virtual ICollection<PontoPeriodo> PontoPeriodos { get; set; }
    }
}
