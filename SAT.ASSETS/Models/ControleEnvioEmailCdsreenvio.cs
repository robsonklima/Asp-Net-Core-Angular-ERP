using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ControleEnvioEmailCDSReenvio")]
    public partial class ControleEnvioEmailCdsreenvio
    {
        [Key]
        [Column("CodControleEnvioEmailCDSReenvio")]
        public int CodControleEnvioEmailCdsreenvio { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public bool Reenviado { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.ControleEnvioEmailCdsreenvios))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.ControleEnvioEmailCdsreenvios))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
