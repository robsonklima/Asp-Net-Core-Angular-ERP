using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ControleEnvioEmailCDS")]
    public partial class ControleEnvioEmailCd
    {
        [Key]
        [Column("CodControleEnvioEmailCDS")]
        public int CodControleEnvioEmailCds { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEnvio { get; set; }
        [StringLength(2000)]
        public string Email { get; set; }
        [StringLength(20)]
        public string CodUsuario { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.ControleEnvioEmailCds))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.ControleEnvioEmailCds))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
