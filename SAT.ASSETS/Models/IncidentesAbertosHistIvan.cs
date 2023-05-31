using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("IncidentesAbertosHistIvan")]
    public partial class IncidentesAbertosHistIvan
    {
        [Column("codIncidentesAbertosHistIvan")]
        public int CodIncidentesAbertosHistIvan { get; set; }
        public int? Qtd { get; set; }
        public int? QtdOrc { get; set; }
        public int? DiaSemana { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
