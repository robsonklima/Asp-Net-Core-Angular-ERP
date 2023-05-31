using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcTotalSlaHsbcFilial
    {
        public int CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        public int? Dentro { get; set; }
        public int? Total { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Percentual { get; set; }
    }
}
