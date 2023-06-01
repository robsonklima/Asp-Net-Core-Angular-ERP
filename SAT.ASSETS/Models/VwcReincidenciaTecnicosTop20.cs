using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReincidenciaTecnicosTop20
    {
        [StringLength(50)]
        public string NomeTecnico { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int? QtdChamadosAtendidos { get; set; }
        public int? QtdChamadosReincidentes { get; set; }
        [Column("PERC", TypeName = "decimal(3, 2)")]
        public decimal? Perc { get; set; }
    }
}
