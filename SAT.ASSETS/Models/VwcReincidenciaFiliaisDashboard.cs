using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReincidenciaFiliaisDashboard
    {
        public int CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        public int? ChamadosMes { get; set; }
        public int? ChamadosMesReincidente { get; set; }
        public int? TotalGeral { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual { get; set; }
    }
}
