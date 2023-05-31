using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPerformance10indicesRegiaoDadosCongelado
    {
        [Required]
        [StringLength(50)]
        public string Regiao { get; set; }
        [StringLength(7)]
        public string AnoMes { get; set; }
        [Column("SLA", TypeName = "decimal(10, 2)")]
        public decimal? Sla { get; set; }
    }
}
