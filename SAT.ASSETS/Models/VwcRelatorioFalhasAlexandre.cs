using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelatorioFalhasAlexandre
    {
        [StringLength(60)]
        public string AnoMes { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Required]
        [StringLength(50)]
        public string LocalAtendimento { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [Column("NumRAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        [Column("TempoRAT")]
        [StringLength(7)]
        public string TempoRat { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
    }
}
