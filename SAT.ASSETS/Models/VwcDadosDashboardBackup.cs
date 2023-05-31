using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDadosDashboardBackup
    {
        [Required]
        [StringLength(1)]
        public string ColunaA { get; set; }
        public int CodFilial { get; set; }
        [Required]
        [Column("FILIAL")]
        [StringLength(50)]
        public string Filial { get; set; }
        [Column("DENTRO")]
        public int? Dentro { get; set; }
        [Column("FORA")]
        public int? Fora { get; set; }
        public int? TotalGeral { get; set; }
        [Column(TypeName = "numeric(15, 1)")]
        public decimal? Percentual { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Reincidencia { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Pendencia { get; set; }
    }
}
