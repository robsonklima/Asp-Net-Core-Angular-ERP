using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioVisaoUsuario")]
    public partial class RelatorioVisaoUsuario
    {
        [Key]
        public int CodRelatorioVisao { get; set; }
        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodRelatorioVisao))]
        [InverseProperty(nameof(RelatorioVisao.RelatorioVisaoUsuarios))]
        public virtual RelatorioVisao CodRelatorioVisaoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.RelatorioVisaoUsuarioCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.RelatorioVisaoUsuarioCodUsuarioNavigations))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
