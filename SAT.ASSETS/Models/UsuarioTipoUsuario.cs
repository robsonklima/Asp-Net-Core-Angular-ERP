using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("UsuarioTipoUsuario")]
    public partial class UsuarioTipoUsuario
    {
        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodTipoUsuario { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }

        [ForeignKey(nameof(CodTipoUsuario))]
        [InverseProperty(nameof(TipoUsuario.UsuarioTipoUsuarios))]
        public virtual TipoUsuario CodTipoUsuarioNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.UsuarioTipoUsuarioCodUsuarioCadastroNavigations))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.UsuarioTipoUsuarioCodUsuarioNavigation))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
