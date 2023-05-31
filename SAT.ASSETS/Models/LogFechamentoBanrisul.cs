using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LogFechamentoBanrisul")]
    public partial class LogFechamentoBanrisul
    {
        [Key]
        [Column("codLogFechamentoBanrisul")]
        public int CodLogFechamentoBanrisul { get; set; }
        [Column("codOS")]
        public int? CodOs { get; set; }
        [Column("dataHoraFechamento", TypeName = "datetime")]
        public DateTime DataHoraFechamento { get; set; }
        [Column("indValidado")]
        public int IndValidado { get; set; }
        [Required]
        [Column("statusFechamento")]
        [StringLength(100)]
        public string StatusFechamento { get; set; }
    }
}
