using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPerformance10indicesRegiaoReincidenciaFilial
    {
        [StringLength(50)]
        public string Regiao { get; set; }
        [StringLength(61)]
        public string AnoMes { get; set; }
        [Column("SLA")]
        public int? Sla { get; set; }
        public int? CodFilial { get; set; }
    }
}
