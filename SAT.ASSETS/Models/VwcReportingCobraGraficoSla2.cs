using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingCobraGraficoSla2
    {
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal? ManutencaoMensal { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Required]
        [Column("CL10_1_5")]
        [StringLength(3)]
        public string Cl1015 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFiltro { get; set; }
    }
}
