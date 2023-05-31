using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBCalcOS")]
    public partial class DispBbcalcO
    {
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? CodEquipContrato { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [StringLength(10)]
        public string NomeFilial { get; set; }
        [Column("CodDispBBCriticidade")]
        public int? CodDispBbcriticidade { get; set; }
        [StringLength(20)]
        public string Regiao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Inicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fim { get; set; }
        public int? IndispMin { get; set; }
        public byte? IndVandalismo { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
