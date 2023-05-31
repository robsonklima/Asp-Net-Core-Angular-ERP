using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ReportScript")]
    public partial class ReportScript
    {
        [Key]
        public int CodReportScript { get; set; }
        public int? CodReportRecorrencia { get; set; }
        [StringLength(100)]
        public string NomeParametro { get; set; }
        [StringLength(100)]
        public string ValorParametro { get; set; }
        [StringLength(100)]
        public string NomeReport { get; set; }
        [StringLength(100)]
        public string DescricaoReport { get; set; }
        [StringLength(100)]
        public string DescricaoAgendamento { get; set; }
        [StringLength(1000)]
        public string EmailRemetente { get; set; }
        [StringLength(1000)]
        public string EmailDestinatario { get; set; }
        public byte IndAtivo { get; set; }
    }
}
