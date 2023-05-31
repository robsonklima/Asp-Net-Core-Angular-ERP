using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ReportPerfil")]
    public partial class ReportPerfil
    {
        [Key]
        public int CodReportPerfil { get; set; }
        public int CodSatPerfil { get; set; }
        public int CodReport { get; set; }
        public int IndAtivo { get; set; }
    }
}
