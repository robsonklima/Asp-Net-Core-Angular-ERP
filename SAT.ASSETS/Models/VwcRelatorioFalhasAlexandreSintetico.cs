using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelatorioFalhasAlexandreSintetico
    {
        [StringLength(60)]
        public string AnoMes { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        public int? Falhas { get; set; }
        [StringLength(61)]
        public string DuracaoFalhas { get; set; }
        public int? TempoMedioFalhas { get; set; }
    }
}
