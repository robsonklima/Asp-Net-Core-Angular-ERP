using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSReabertura")]
    public partial class Osreabertura
    {
        [Key]
        [Column("CodOSReabertura")]
        public int CodOsreabertura { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodMotivoReaberturaOS")]
        public int CodMotivoReaberturaOs { get; set; }
        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataReabertura { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }

        [ForeignKey(nameof(CodMotivoReaberturaOs))]
        [InverseProperty(nameof(MotivoReaberturaO.Osreaberturas))]
        public virtual MotivoReaberturaO CodMotivoReaberturaOsNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.Osreaberturas))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.Osreaberturas))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
