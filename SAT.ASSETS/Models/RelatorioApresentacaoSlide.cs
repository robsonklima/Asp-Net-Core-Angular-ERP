using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioApresentacaoSlide")]
    public partial class RelatorioApresentacaoSlide
    {
        public RelatorioApresentacaoSlide()
        {
            RelatorioApresentacaoSlideVisaos = new HashSet<RelatorioApresentacaoSlideVisao>();
        }

        [Key]
        public int CodSlide { get; set; }
        public int CodApresentacao { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeSlide { get; set; }
        public int Ordem { get; set; }
        [Required]
        [StringLength(10)]
        public string Intervalo { get; set; }

        [ForeignKey(nameof(CodApresentacao))]
        [InverseProperty(nameof(RelatorioApresentacao.RelatorioApresentacaoSlides))]
        public virtual RelatorioApresentacao CodApresentacaoNavigation { get; set; }
        [InverseProperty(nameof(RelatorioApresentacaoSlideVisao.CodSlideNavigation))]
        public virtual ICollection<RelatorioApresentacaoSlideVisao> RelatorioApresentacaoSlideVisaos { get; set; }
    }
}
