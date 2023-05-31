using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SPA_KPI")]
    public partial class SpaKpi
    {
        [Column("CodSPA")]
        public int CodSpa { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodCliente { get; set; }
        public int CodEquipContrato { get; set; }
        public int CodTipoIntervencao { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime DataHoraAberturaOs { get; set; }
        [Required]
        [Column("StatusSPA")]
        [StringLength(15)]
        public string StatusSpa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraProcessamento { get; set; }
        [Column("ReincSPA")]
        public int? ReincSpa { get; set; }
        public int? CodStatusServico { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSolucao { get; set; }
    }
}
