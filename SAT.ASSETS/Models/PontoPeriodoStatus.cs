using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoPeriodoStatus")]
    public partial class PontoPeriodoStatus
    {
        public PontoPeriodoStatus()
        {
            PontoPeriodos = new HashSet<PontoPeriodo>();
        }

        [Key]
        public int CodPontoPeriodoStatus { get; set; }
        [Required]
        [StringLength(30)]
        public string NomePeriodoStatus { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(PontoPeriodo.CodPontoPeriodoStatusNavigation))]
        public virtual ICollection<PontoPeriodo> PontoPeriodos { get; set; }
    }
}
