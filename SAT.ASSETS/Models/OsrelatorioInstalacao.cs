using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSRelatorioInstalacao")]
    public partial class OsrelatorioInstalacao
    {
        [Key]
        [Column("CodOSRelatorioInstalacao")]
        public int CodOsrelatorioInstalacao { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodOSRelatorioInstalacaoItem")]
        public int CodOsrelatorioInstalacaoItem { get; set; }
        public int IndStatus { get; set; }
        [StringLength(500)]
        public string Detalhe { get; set; }
    }
}
