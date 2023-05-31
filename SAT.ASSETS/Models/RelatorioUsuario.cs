using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioUsuario")]
    public partial class RelatorioUsuario
    {
        [Key]
        public int CodRelatorio { get; set; }
        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodRelatorio))]
        [InverseProperty(nameof(Relatorio.RelatorioUsuarios))]
        public virtual Relatorio CodRelatorioNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.RelatorioUsuarioCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.RelatorioUsuarioCodUsuarioNavigations))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
