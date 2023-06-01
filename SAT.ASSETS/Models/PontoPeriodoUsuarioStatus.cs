using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoPeriodoUsuarioStatus")]
    public partial class PontoPeriodoUsuarioStatus
    {
        public PontoPeriodoUsuarioStatus()
        {
            PontoPeriodoUsuarios = new HashSet<PontoPeriodoUsuario>();
        }

        [Key]
        public int CodPontoPeriodoUsuarioStatus { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [InverseProperty(nameof(PontoPeriodoUsuario.CodPontoPeriodoUsuarioStatusNavigation))]
        public virtual ICollection<PontoPeriodoUsuario> PontoPeriodoUsuarios { get; set; }
    }
}
