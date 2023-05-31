using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AgendamentoOSLog")]
    public partial class AgendamentoOslog
    {
        [Key]
        [Column("CodAgendamentoOSLog")]
        public int CodAgendamentoOslog { get; set; }
        public int? CodAgendamento { get; set; }
        public int? CodMotivo { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [StringLength(20)]
        public string CodUsuarioAgendamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraUsuAgendamento { get; set; }
        [StringLength(20)]
        public string CodUsuarioExclusao { get; set; }
    }
}
