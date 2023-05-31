using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogAlertum
    {
        [StringLength(50)]
        public string Servidor { get; set; }
        [StringLength(50)]
        public string Item { get; set; }
        [StringLength(1045)]
        public string Mensagem { get; set; }
        public double? EspacoEmGb { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraProcessamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DatahoraCad { get; set; }
        [StringLength(7)]
        public string OciosidadeFormatado { get; set; }
        public int? OciosidadeMinutos { get; set; }
        [StringLength(20)]
        public string Tipo { get; set; }
        [StringLength(10)]
        public string Disco { get; set; }
        public double? TamanhoEmGb { get; set; }
    }
}
