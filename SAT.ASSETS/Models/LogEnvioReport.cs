using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class LogEnvioReport
    {
        [Column("codLogServico")]
        public int CodLogServico { get; set; }
        [StringLength(500)]
        public string NomeRelatorio { get; set; }
        [StringLength(2000)]
        public string Emails { get; set; }
        [StringLength(500)]
        public string Descricao { get; set; }
        [StringLength(500)]
        public string UltimoStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCriacaoRelatorio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataModificacaoRelatorio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataModificacaoAgendamento { get; set; }
    }
}
