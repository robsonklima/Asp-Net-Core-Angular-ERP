using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSHistPOS")]
    public partial class OshistPo
    {
        [Key]
        [Column("CodOSHistPOS")]
        public int CodOshistPos { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column("CodAcaoOS")]
        public int CodAcaoOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        [StringLength(2000)]
        public string Imagem { get; set; }

        [ForeignKey(nameof(CodAcaoOs))]
        [InverseProperty(nameof(AcaoO.OshistPos))]
        public virtual AcaoO CodAcaoOsNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.OshistPos))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.OshistPos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
