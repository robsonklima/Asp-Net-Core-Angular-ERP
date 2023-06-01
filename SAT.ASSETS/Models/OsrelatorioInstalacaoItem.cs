using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OSRelatorioInstalacaoItem")]
    public partial class OsrelatorioInstalacaoItem
    {
        [Column("codOSRelatorioInstalacaoItem")]
        public int CodOsrelatorioInstalacaoItem { get; set; }
        [Required]
        [StringLength(50)]
        public string Item { get; set; }
        [Column("indAtivo")]
        public int IndAtivo { get; set; }
    }
}
