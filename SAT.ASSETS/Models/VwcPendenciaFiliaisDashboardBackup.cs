using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPendenciaFiliaisDashboardBackup
    {
        public int CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        public int? ChamadosMes { get; set; }
        public int? ChamadosMesPecasPendentes { get; set; }
        public int? TotalGeral { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual { get; set; }
    }
}
