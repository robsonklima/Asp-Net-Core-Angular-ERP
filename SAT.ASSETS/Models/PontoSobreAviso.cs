using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoSobreAviso")]
    public partial class PontoSobreAviso
    {
        [Key]
        public int CodPontoSobreAviso { get; set; }
        public int CodPontoPeriodo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioPonto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraFim { get; set; }
        [StringLength(300)]
        public string Observacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }

        [ForeignKey(nameof(CodPontoPeriodo))]
        [InverseProperty(nameof(PontoPeriodo.PontoSobreAvisos))]
        public virtual PontoPeriodo CodPontoPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioPonto))]
        [InverseProperty(nameof(Usuario.PontoSobreAvisos))]
        public virtual Usuario CodUsuarioPontoNavigation { get; set; }
    }
}
