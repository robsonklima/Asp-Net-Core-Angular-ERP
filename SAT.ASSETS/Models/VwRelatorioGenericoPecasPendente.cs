using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwRelatorioGenericoPecasPendente
    {
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(60)]
        public string AnoMes { get; set; }
        public int Geral { get; set; }
        public int Dentro { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column("RAT")]
        [StringLength(20)]
        public string Rat { get; set; }
    }
}
