using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwRepasseAutorizadum
    {
        public int? AnoMes { get; set; }
        [Required]
        [StringLength(50)]
        public string Autorizada { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        [Required]
        [StringLength(50)]
        public string Local { get; set; }
        [StringLength(20)]
        public string Série { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorRepasse { get; set; }
    }
}
