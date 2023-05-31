﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AgendamentoOS_ARQ_MORTO")]
    public partial class AgendamentoOsArqMorto
    {
        [Key]
        public int CodAgendamento { get; set; }
        public int? CodMotivo { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [StringLength(20)]
        public string CodUsuarioAgendamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraUsuAgendamento { get; set; }
    }
}
