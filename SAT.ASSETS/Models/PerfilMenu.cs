using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PerfilMenu")]
    public partial class PerfilMenu
    {
        [Key]
        public int CodPerfil { get; set; }
        [Key]
        public int CodMenu { get; set; }
        public int? CodSistema { get; set; }

        [ForeignKey(nameof(CodMenu))]
        [InverseProperty(nameof(Menu.PerfilMenus))]
        public virtual Menu CodMenuNavigation { get; set; }
        [ForeignKey(nameof(CodPerfil))]
        [InverseProperty(nameof(Perfil.PerfilMenus))]
        public virtual Perfil CodPerfilNavigation { get; set; }
    }
}
