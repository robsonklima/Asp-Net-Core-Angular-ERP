using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioApresentacao")]
    public partial class RelatorioApresentacao
    {
        public RelatorioApresentacao()
        {
            RelatorioApresentacaoSlides = new HashSet<RelatorioApresentacaoSlide>();
        }

        [Key]
        public int CodApresentacao { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeApresentacao { get; set; }
        [StringLength(200)]
        public string DescApresentacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.RelatorioApresentacaoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.RelatorioApresentacaoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(RelatorioApresentacaoSlide.CodApresentacaoNavigation))]
        public virtual ICollection<RelatorioApresentacaoSlide> RelatorioApresentacaoSlides { get; set; }
    }
}
