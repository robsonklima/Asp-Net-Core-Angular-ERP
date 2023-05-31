using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MotivoReaberturaOS")]
    public partial class MotivoReaberturaO
    {
        public MotivoReaberturaO()
        {
            Osreaberturas = new HashSet<Osreabertura>();
        }

        [Key]
        [Column("CodMotivoReaberturaOS")]
        public int CodMotivoReaberturaOs { get; set; }
        [Required]
        [StringLength(50)]
        public string MotivoReabertura { get; set; }
        public bool IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.MotivoReaberturaOs))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [InverseProperty(nameof(Osreabertura.CodMotivoReaberturaOsNavigation))]
        public virtual ICollection<Osreabertura> Osreaberturas { get; set; }
    }
}
