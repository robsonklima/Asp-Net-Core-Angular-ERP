using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OSRelatorioInstalacaoNaoConformidadeItem")]
    public partial class OsrelatorioInstalacaoNaoConformidadeItem
    {
        [Column("CodOSRelatorioNaoConformidadeItem")]
        public int CodOsrelatorioNaoConformidadeItem { get; set; }
        [Required]
        [StringLength(50)]
        public string Item { get; set; }
        public byte IndAtivo { get; set; }
    }
}
