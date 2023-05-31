using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("InstalCampo")]
    public partial class InstalCampo
    {
        [Column("codInstalCampo")]
        public int CodInstalCampo { get; set; }
        [Column("numInstalCampo")]
        public int? NumInstalCampo { get; set; }
        [Column("nomeInstalCampo")]
        [StringLength(300)]
        public string NomeInstalCampo { get; set; }
    }
}
