﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDadosDashboardCliente
    {
        [StringLength(6)]
        public string AnoMes { get; set; }
        public int? CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Reincidencia { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Pendencia { get; set; }
    }
}