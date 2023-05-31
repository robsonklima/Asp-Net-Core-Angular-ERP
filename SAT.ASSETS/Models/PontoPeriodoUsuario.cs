using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoPeriodoUsuario")]
    public partial class PontoPeriodoUsuario
    {
        [Key]
        public int CodPontoPeriodoUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        public int CodPontoPeriodoUsuarioStatus { get; set; }

        [ForeignKey(nameof(CodPontoPeriodo))]
        [InverseProperty(nameof(PontoPeriodo.PontoPeriodoUsuarios))]
        public virtual PontoPeriodo CodPontoPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoPeriodoUsuarioStatus))]
        [InverseProperty(nameof(PontoPeriodoUsuarioStatus.PontoPeriodoUsuarios))]
        public virtual PontoPeriodoUsuarioStatus CodPontoPeriodoUsuarioStatusNavigation { get; set; }
    }
}
