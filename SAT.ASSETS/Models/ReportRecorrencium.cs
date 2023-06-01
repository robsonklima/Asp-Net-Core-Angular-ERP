using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ReportRecorrencium
    {
        [Key]
        public int CodReportRecorrencia { get; set; }
        public int CodReportAgendamento { get; set; }
        [Required]
        [StringLength(255)]
        public string HoraInicioEnvio { get; set; }
        [Required]
        [StringLength(255)]
        public string HoraFimEnvio { get; set; }
        [StringLength(19)]
        public string DataHoraEnviado { get; set; }
        [StringLength(8)]
        public string IntervaloHoras { get; set; }
        [StringLength(255)]
        public string IntervaloDiasPeriodico { get; set; }
        [StringLength(255)]
        public string IntervaloDiaSemana { get; set; }
        [StringLength(255)]
        public string IntervaloDiaMes { get; set; }
        public int IndAtivo { get; set; }
    }
}
