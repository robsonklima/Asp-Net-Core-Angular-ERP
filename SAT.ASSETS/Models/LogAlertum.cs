using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class LogAlertum
    {
        [Key]
        public int CodLogAlerta { get; set; }
        [StringLength(50)]
        public string Servidor { get; set; }
        [StringLength(50)]
        public string Item { get; set; }
        [StringLength(1000)]
        public string Mensagem { get; set; }
        [StringLength(20)]
        public string Tipo { get; set; }
        public double? EspacoEmGb { get; set; }
        public double? TamanhoEmGb { get; set; }
        [StringLength(10)]
        public string Disco { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraProcessamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
