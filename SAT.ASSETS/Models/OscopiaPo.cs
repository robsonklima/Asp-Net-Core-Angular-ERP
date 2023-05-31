using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSCopiaPOS")]
    public partial class OscopiaPo
    {
        [Key]
        [Column("CodOSCopiaPOS")]
        public int CodOscopiaPos { get; set; }
        [Column("CodOSOrigem")]
        public int CodOsorigem { get; set; }
        [Column("CodOSDestino")]
        public int CodOsdestino { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodOsdestino))]
        [InverseProperty(nameof(O.OscopiaPoCodOsdestinoNavigations))]
        public virtual O CodOsdestinoNavigation { get; set; }
        [ForeignKey(nameof(CodOsorigem))]
        [InverseProperty(nameof(O.OscopiaPoCodOsorigemNavigations))]
        public virtual O CodOsorigemNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.OscopiaPos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
