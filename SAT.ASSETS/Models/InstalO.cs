using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalOS")]
    public partial class InstalO
    {
        [Key]
        public int CodInstalacao { get; set; }
        [Key]
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodRAT")]
        public int? CodRat { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodInstalacao))]
        [InverseProperty(nameof(Instalacao.InstalOs))]
        public virtual Instalacao CodInstalacaoNavigation { get; set; }
        [ForeignKey(nameof(CodRat))]
        [InverseProperty(nameof(Rat.InstalOs))]
        public virtual Rat CodRatNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.InstalOs))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
    }
}
