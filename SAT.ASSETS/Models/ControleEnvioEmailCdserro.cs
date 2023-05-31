using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ControleEnvioEmailCDSErro")]
    public partial class ControleEnvioEmailCdserro
    {
        [Key]
        [Column("CodControleEnvioEmailCDSErro")]
        public int CodControleEnvioEmailCdserro { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEnvio { get; set; }
        [Required]
        [StringLength(2000)]
        public string Erro { get; set; }
        [StringLength(2000)]
        public string Email { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.ControleEnvioEmailCdserros))]
        public virtual O CodOsNavigation { get; set; }
    }
}
