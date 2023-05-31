using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DashboardDisponibilidade")]
    public partial class DashboardDisponibilidade
    {
        [StringLength(50)]
        public string Regiao { get; set; }
        public int? Criticidade { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        [Column(TypeName = "decimal(10, 1)")]
        public decimal? PrcTotalFilial { get; set; }
        public int? CodFilial { get; set; }
    }
}
