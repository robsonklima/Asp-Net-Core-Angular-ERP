using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcMonitoramentoTempoAberturaChamadoCliente
    {
        public int CodCliente { get; set; }
        [Required]
        [StringLength(30)]
        public string Cliente { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int? TempoUltimaAbertura { get; set; }
        [StringLength(7)]
        public string TempoEmHorasUltimaAbertura { get; set; }
    }
}
