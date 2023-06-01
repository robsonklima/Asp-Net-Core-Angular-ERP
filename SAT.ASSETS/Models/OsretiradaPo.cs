using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSRetiradaPOS")]
    public partial class OsretiradaPo
    {
        [Key]
        [Column("CodOSRetiradaPOS")]
        public int CodOsretiradaPos { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodOSInstalacao")]
        public int CodOsinstalacao { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.OsretiradaPoCodOsNavigations))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodOsinstalacao))]
        [InverseProperty(nameof(O.OsretiradaPoCodOsinstalacaoNavigations))]
        public virtual O CodOsinstalacaoNavigation { get; set; }
    }
}
