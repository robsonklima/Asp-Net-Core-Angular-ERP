using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MenuPOS")]
    public partial class MenuPo
    {
        public MenuPo()
        {
            InverseCodMenuPaiNavigation = new HashSet<MenuPo>();
            TipoUsuarioMenuPos = new HashSet<TipoUsuarioMenuPo>();
        }

        [Key]
        public int CodMenu { get; set; }
        public int? CodMenuPai { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeMenu { get; set; }
        [StringLength(50)]
        public string Controller { get; set; }
        [StringLength(50)]
        public string Action { get; set; }
        [Required]
        [StringLength(50)]
        public string Icon { get; set; }
        [StringLength(50)]
        public string Url { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey(nameof(CodMenuPai))]
        [InverseProperty(nameof(MenuPo.InverseCodMenuPaiNavigation))]
        public virtual MenuPo CodMenuPaiNavigation { get; set; }
        [InverseProperty(nameof(MenuPo.CodMenuPaiNavigation))]
        public virtual ICollection<MenuPo> InverseCodMenuPaiNavigation { get; set; }
        [InverseProperty(nameof(TipoUsuarioMenuPo.CodMenuNavigation))]
        public virtual ICollection<TipoUsuarioMenuPo> TipoUsuarioMenuPos { get; set; }
    }
}
