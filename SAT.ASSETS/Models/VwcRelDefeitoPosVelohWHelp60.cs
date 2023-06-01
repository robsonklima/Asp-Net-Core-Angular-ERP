using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelDefeitoPosVelohWHelp60
    {
        [Required]
        [StringLength(200)]
        public string Defeito { get; set; }
        public int? Quantidade { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? Percentual { get; set; }
    }
}
