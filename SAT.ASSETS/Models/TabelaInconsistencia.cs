using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class TabelaInconsistencia
    {
        public int? CodPontoPeriodo { get; set; }
        public int? CodDescricaoMotivoInconsistencia { get; set; }
        [StringLength(255)]
        public string DescricaoMotivoInconsistencia { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int? QtdInconsistencias { get; set; }
        public int? QtdColaboradores { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? MediaQtdInconsistencias { get; set; }
        public int? QtdTotalColaboradores { get; set; }
    }
}
