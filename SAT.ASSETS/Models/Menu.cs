using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Menu")]
    public partial class Menu
    {
        public Menu()
        {
            PerfilMenus = new HashSet<PerfilMenu>();
            UsuarioMenus = new HashSet<UsuarioMenu>();
        }

        [Key]
        public int CodMenu { get; set; }
        public int? CodMenuPai { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeMenu { get; set; }
        public int Nivel { get; set; }
        public int? OrdemClassif { get; set; }
        public string Funcao { get; set; }
        public byte IndSmartCard { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public byte IndAtivo { get; set; }
        public int? CodTraducao { get; set; }
        public int? CodSistema { get; set; }
        public byte? IndMobile { get; set; }

        [ForeignKey(nameof(CodSistema))]
        [InverseProperty(nameof(Sistema.Menus))]
        public virtual Sistema CodSistemaNavigation { get; set; }
        [InverseProperty(nameof(PerfilMenu.CodMenuNavigation))]
        public virtual ICollection<PerfilMenu> PerfilMenus { get; set; }
        [InverseProperty(nameof(UsuarioMenu.CodMenuNavigation))]
        public virtual ICollection<UsuarioMenu> UsuarioMenus { get; set; }
    }
}
