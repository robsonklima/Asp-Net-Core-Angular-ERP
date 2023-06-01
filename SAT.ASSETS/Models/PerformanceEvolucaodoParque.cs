using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_EvolucaodoParque")]
    public partial class PerformanceEvolucaodoParque
    {
        [Column("CodPerformance_EvolucaodoParque")]
        public int CodPerformanceEvolucaodoParque { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        public int? Qtd { get; set; }
    }
}
