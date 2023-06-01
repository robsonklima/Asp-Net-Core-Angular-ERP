using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSRessarcirCobrancaSicrediPOS")]
    public partial class OsressarcirCobrancaSicrediPo
    {
        [Key]
        [Column("CodOSRessarcirCobrancaSicredi")]
        public int CodOsressarcirCobrancaSicredi { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MesDesconto { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Required]
        [StringLength(2000)]
        public string MotivoRessarcimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.OsressarcirCobrancaSicrediPos))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.OsressarcirCobrancaSicrediPos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
