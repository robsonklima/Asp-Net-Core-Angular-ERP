using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ReportParametro")]
    public partial class ReportParametro
    {
        public ReportParametro()
        {
            ReportRelacaos = new HashSet<ReportRelacao>();
        }

        [Key]
        public int CodReportParametro { get; set; }
        [Required]
        [StringLength(300)]
        public string NomeParametro { get; set; }
        [Required]
        [StringLength(300)]
        public string ValorParametro { get; set; }
        public int IndAtivo { get; set; }

        [InverseProperty(nameof(ReportRelacao.CodReportParametroNavigation))]
        public virtual ICollection<ReportRelacao> ReportRelacaos { get; set; }
    }
}
