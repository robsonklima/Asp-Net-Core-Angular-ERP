using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingCobraSlaComparacaoAut
    {
        [Column("OS")]
        public int Os { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("SLACobra")]
        [StringLength(10)]
        public string Slacobra { get; set; }
        [Column("StatusOS")]
        [StringLength(50)]
        public string StatusOs { get; set; }
        [Column("DataAberturaOS", TypeName = "datetime")]
        public DateTime? DataAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAberturaCobra { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamentoCobra { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFechamentoCobra { get; set; }
        [Column("DataFimSLA", TypeName = "datetime")]
        public DateTime? DataFimSla { get; set; }
        [Column("DataFimSLACobra", TypeName = "datetime")]
        public DateTime? DataFimSlacobra { get; set; }
        [Column("KM")]
        public int? Km { get; set; }
        [Column("KMCobra")]
        public int? Kmcobra { get; set; }
        [Required]
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Required]
        [Column("StatusSLACobra")]
        [StringLength(6)]
        public string StatusSlacobra { get; set; }
        [Required]
        [Column("MOTIVO")]
        [StringLength(30)]
        public string Motivo { get; set; }
        [Required]
        [Column("AÇÃO")]
        [StringLength(18)]
        public string Ação { get; set; }
        [Column("DataFimSLA_SemCofre")]
        [StringLength(20)]
        public string DataFimSlaSemCofre { get; set; }
        [Column("DataFimSLA_SemVand")]
        [StringLength(20)]
        public string DataFimSlaSemVand { get; set; }
        [Column("DataFimSLA_SemCopa")]
        [StringLength(20)]
        public string DataFimSlaSemCopa { get; set; }
    }
}
