using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ReportRelacao")]
    public partial class ReportRelacao
    {
        [Key]
        public int CodReportRelacao { get; set; }
        public int CodReportAgendamento { get; set; }
        public int CodReport { get; set; }
        public int? CodReportParametro { get; set; }
        public int IndAtivo { get; set; }

        [ForeignKey(nameof(CodReportAgendamento))]
        [InverseProperty(nameof(ReportAgendamento.ReportRelacaos))]
        public virtual ReportAgendamento CodReportAgendamentoNavigation { get; set; }
        [ForeignKey(nameof(CodReport))]
        [InverseProperty(nameof(Report.ReportRelacaos))]
        public virtual Report CodReportNavigation { get; set; }
        [ForeignKey(nameof(CodReportParametro))]
        [InverseProperty(nameof(ReportParametro.ReportRelacaos))]
        public virtual ReportParametro CodReportParametroNavigation { get; set; }
    }
}
