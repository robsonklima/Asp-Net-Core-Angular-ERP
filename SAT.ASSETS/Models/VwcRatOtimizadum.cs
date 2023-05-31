using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRatOtimizadum
    {
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSolucaoValida { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolucao { get; set; }
        public int? CodStatusServico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
