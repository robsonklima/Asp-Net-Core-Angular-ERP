﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempSLACEF_5855")]
    public partial class TempSlacef5855
    {
        [StringLength(5)]
        public string CodFilial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPercentual { get; set; }
        public int? Dentro { get; set; }
        public int? Total { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual { get; set; }
    }
}