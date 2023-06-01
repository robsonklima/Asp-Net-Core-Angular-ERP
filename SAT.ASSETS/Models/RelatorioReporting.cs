using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioReporting")]
    public partial class RelatorioReporting
    {
        [Key]
        public int CodRelatorioReporting { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeRelatorioReporting { get; set; }
        [Required]
        [StringLength(300)]
        public string DescRelatorioReporting { get; set; }
        [Required]
        [StringLength(1000)]
        public string LinkRelatorioReporting { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
