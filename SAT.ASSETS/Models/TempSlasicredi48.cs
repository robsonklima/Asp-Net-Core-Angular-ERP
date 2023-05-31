﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempSLASICREDI48")]
    public partial class TempSlasicredi48
    {
        [Column("SiglaUF")]
        [StringLength(5)]
        public string SiglaUf { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPercentual { get; set; }
        public int? Dentro { get; set; }
        public int? Total { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual { get; set; }
    }
}