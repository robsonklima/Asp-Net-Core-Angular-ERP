using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioVisao")]
    public partial class RelatorioVisao
    {
        public RelatorioVisao()
        {
            RelatorioApresentacaoSlideVisaos = new HashSet<RelatorioApresentacaoSlideVisao>();
            RelatorioVisaoUsuarios = new HashSet<RelatorioVisaoUsuario>();
        }

        [Key]
        public int CodRelatorioVisao { get; set; }
        public int CodRelatorio { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeRelatorioVisao { get; set; }
        [Required]
        [Column("XMLData", TypeName = "text")]
        public string Xmldata { get; set; }
        public byte IndTipoVisao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodRelatorio))]
        [InverseProperty(nameof(Relatorio.RelatorioVisaos))]
        public virtual Relatorio CodRelatorioNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.RelatorioVisaoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.RelatorioVisaoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(RelatorioApresentacaoSlideVisao.CodRelatorioVisaoNavigation))]
        public virtual ICollection<RelatorioApresentacaoSlideVisao> RelatorioApresentacaoSlideVisaos { get; set; }
        [InverseProperty(nameof(RelatorioVisaoUsuario.CodRelatorioVisaoNavigation))]
        public virtual ICollection<RelatorioVisaoUsuario> RelatorioVisaoUsuarios { get; set; }
    }
}
