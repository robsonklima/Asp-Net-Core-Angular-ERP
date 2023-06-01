using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBCalcEquipamentoContrato")]
    public partial class DispBbcalcEquipamentoContrato
    {
        public int? CodEquipContrato { get; set; }
        [StringLength(18)]
        public string NumSerie { get; set; }
        public int? CodEquip { get; set; }
        [StringLength(10)]
        public string Modelo { get; set; }
        public int? Criticidade { get; set; }
        [Column("CodDispBBRegiao")]
        public int? CodDispBbregiao { get; set; }
        [StringLength(30)]
        public string Regiao { get; set; }
        public int? IndispMin { get; set; }
        public int? MinTotais { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DispPerc { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? IndispPerc { get; set; }
        public double? Valor { get; set; }
        public double? IndTaa { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
