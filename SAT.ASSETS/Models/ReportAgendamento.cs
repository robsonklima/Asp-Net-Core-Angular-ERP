using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ReportAgendamento")]
    public partial class ReportAgendamento
    {
        public ReportAgendamento()
        {
            ReportRelacaos = new HashSet<ReportRelacao>();
        }

        [Key]
        public int CodReportAgendamento { get; set; }
        public int CodReportCatalogoEmail { get; set; }
        [Required]
        [StringLength(255)]
        public string DescricaoReportAgendamento { get; set; }
        public int IndAtivo { get; set; }

        [InverseProperty(nameof(ReportRelacao.CodReportAgendamentoNavigation))]
        public virtual ICollection<ReportRelacao> ReportRelacaos { get; set; }
    }
}
