using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SessaoUsuario")]
    public partial class SessaoUsuario
    {
        [Key]
        public int CodSessaoUsuario { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
        [Required]
        [Column("IP")]
        [StringLength(25)]
        public string Ip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.SessaoUsuarios))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
