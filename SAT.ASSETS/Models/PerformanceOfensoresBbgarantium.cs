using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_OfensoresBBGarantia")]
    public partial class PerformanceOfensoresBbgarantium
    {
        [Column("CodPerformance_Ofensores")]
        public int CodPerformanceOfensores { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [StringLength(12)]
        public string Tipo { get; set; }
        [StringLength(50)]
        public string Causa { get; set; }
        [Column("QTD")]
        public int? Qtd { get; set; }
        public int? Perc { get; set; }
    }
}
