using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("UsuarioNovaSenha")]
    public partial class UsuarioNovaSenha
    {
        [Key]
        public int CodUsuarioNovaSenha { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataSolicitacao { get; set; }
        [Required]
        [StringLength(50)]
        public string Token { get; set; }
        public bool EmailEnviado { get; set; }
        public bool Desbloqueio { get; set; }
        [Required]
        [StringLength(500)]
        public string Url { get; set; }

        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.UsuarioNovaSenhas))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
