using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioApresentacaoSlideVisao")]
    public partial class RelatorioApresentacaoSlideVisao
    {
        [Key]
        public int CodSlide { get; set; }
        [Key]
        public int CodRelatorioVisao { get; set; }
        public int Ordem { get; set; }

        [ForeignKey(nameof(CodRelatorioVisao))]
        [InverseProperty(nameof(RelatorioVisao.RelatorioApresentacaoSlideVisaos))]
        public virtual RelatorioVisao CodRelatorioVisaoNavigation { get; set; }
        [ForeignKey(nameof(CodSlide))]
        [InverseProperty(nameof(RelatorioApresentacaoSlide.RelatorioApresentacaoSlideVisaos))]
        public virtual RelatorioApresentacaoSlide CodSlideNavigation { get; set; }
    }
}
