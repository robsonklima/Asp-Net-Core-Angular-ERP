using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoUsuarioMenuPOS")]
    public partial class TipoUsuarioMenuPo
    {
        [Key]
        [Column("CodTipoUsuarioMenuPOS")]
        public int CodTipoUsuarioMenuPos { get; set; }
        public int CodTipoUsuario { get; set; }
        public int CodMenu { get; set; }

        [ForeignKey(nameof(CodMenu))]
        [InverseProperty(nameof(MenuPo.TipoUsuarioMenuPos))]
        public virtual MenuPo CodMenuNavigation { get; set; }
        [ForeignKey(nameof(CodTipoUsuario))]
        [InverseProperty(nameof(TipoUsuario.TipoUsuarioMenuPos))]
        public virtual TipoUsuario CodTipoUsuarioNavigation { get; set; }
    }
}
