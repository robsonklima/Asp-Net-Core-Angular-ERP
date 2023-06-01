using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioReportingUsuario")]
    public partial class RelatorioReportingUsuario
    {
        [Key]
        public int CodRelatorioReportingUsuario { get; set; }
        public int CodRelatorioReporting { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
    }
}
