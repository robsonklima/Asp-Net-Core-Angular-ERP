using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ControleEnvioEmailVipErro")]
    public partial class ControleEnvioEmailVipErro
    {
        [Key]
        public int CodControleEnvioEmailVipErro { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataEnvio { get; set; }
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        [Required]
        [StringLength(2000)]
        public string Erro { get; set; }
        [StringLength(2000)]
        public string Email { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.ControleEnvioEmailVipErros))]
        public virtual O CodOsNavigation { get; set; }
    }
}
