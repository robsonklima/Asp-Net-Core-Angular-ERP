using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempSLACEFContrato")]
    public partial class TempSlacefcontrato
    {
        [StringLength(30)]
        public string NomeContrato1 { get; set; }
        [Column("SiglaUF1")]
        [StringLength(5)]
        public string SiglaUf1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPercentual1 { get; set; }
        public int? Dentro1 { get; set; }
        public int? Total1 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual1 { get; set; }
        [StringLength(30)]
        public string NomeContrato2 { get; set; }
        [Column("SiglaUF2")]
        [StringLength(5)]
        public string SiglaUf2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPercentual2 { get; set; }
        public int? Dentro2 { get; set; }
        public int? Total2 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual2 { get; set; }
        [StringLength(30)]
        public string NomeContrato3 { get; set; }
        [Column("SiglaUF3")]
        [StringLength(5)]
        public string SiglaUf3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPercentual3 { get; set; }
        public int? Dentro3 { get; set; }
        public int? Total3 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual3 { get; set; }
    }
}
