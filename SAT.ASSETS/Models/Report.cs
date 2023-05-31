using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Report")]
    public partial class Report
    {
        public Report()
        {
            ReportRelacaos = new HashSet<ReportRelacao>();
        }

        [Key]
        public int CodReport { get; set; }
        [Required]
        [StringLength(200)]
        public string NomeReport { get; set; }
        [Required]
        [StringLength(100)]
        public string DescricaoReport { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(ReportRelacao.CodReportNavigation))]
        public virtual ICollection<ReportRelacao> ReportRelacaos { get; set; }
    }
}
