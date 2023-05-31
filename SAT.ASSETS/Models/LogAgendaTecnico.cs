using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LogAgendaTecnico")]
    public partial class LogAgendaTecnico
    {
        [Column("codLogAgendaTecnico")]
        public int CodLogAgendaTecnico { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAgenda { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
    }
}
