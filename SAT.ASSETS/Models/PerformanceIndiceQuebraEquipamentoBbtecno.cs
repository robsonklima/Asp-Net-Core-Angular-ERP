using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_IndiceQuebraEquipamentoBBTecno")]
    public partial class PerformanceIndiceQuebraEquipamentoBbtecno
    {
        [Column("CodPerformance_IndiceQuebra")]
        public int CodPerformanceIndiceQuebra { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        [StringLength(50)]
        public string Tipo { get; set; }
        public int? Perc { get; set; }
    }
}
