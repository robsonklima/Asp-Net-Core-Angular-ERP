using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoUsuario")]
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            TipoUsuarioMenuPos = new HashSet<TipoUsuarioMenuPo>();
            UsuarioTipoUsuarios = new HashSet<UsuarioTipoUsuario>();
        }

        [Key]
        public int CodTipoUsuario { get; set; }
        [Required]
        [StringLength(100)]
        public string NomeTipoUsuario { get; set; }
        public bool Ativo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.TipoUsuarios))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [InverseProperty(nameof(TipoUsuarioMenuPo.CodTipoUsuarioNavigation))]
        public virtual ICollection<TipoUsuarioMenuPo> TipoUsuarioMenuPos { get; set; }
        [InverseProperty(nameof(UsuarioTipoUsuario.CodTipoUsuarioNavigation))]
        public virtual ICollection<UsuarioTipoUsuario> UsuarioTipoUsuarios { get; set; }
    }
}
