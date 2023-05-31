using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ReportCatalogoEmail")]
    public partial class ReportCatalogoEmail
    {
        [Key]
        public int CodReportCatalogoEmail { get; set; }
        [Required]
        [StringLength(50)]
        public string EmailRemetente { get; set; }
        [Required]
        [StringLength(1000)]
        public string EmailDestinatario { get; set; }
        public int IndAtivo { get; set; }
    }
}
