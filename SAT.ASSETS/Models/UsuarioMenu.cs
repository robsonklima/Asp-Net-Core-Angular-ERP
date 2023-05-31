using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("UsuarioMenu")]
    public partial class UsuarioMenu
    {
        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Key]
        public int CodMenu { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodMenu))]
        [InverseProperty(nameof(Menu.UsuarioMenus))]
        public virtual Menu CodMenuNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.UsuarioMenus))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
