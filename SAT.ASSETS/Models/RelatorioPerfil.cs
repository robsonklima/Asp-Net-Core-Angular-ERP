using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioPerfil")]
    public partial class RelatorioPerfil
    {
        [Key]
        public int CodRelatorio { get; set; }
        [Key]
        public int CodPerfil { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodPerfil))]
        [InverseProperty(nameof(Perfil.RelatorioPerfils))]
        public virtual Perfil CodPerfilNavigation { get; set; }
        [ForeignKey(nameof(CodRelatorio))]
        [InverseProperty(nameof(Relatorio.RelatorioPerfils))]
        public virtual Relatorio CodRelatorioNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.RelatorioPerfils))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
    }
}
