using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TicketLogPedidoCredito")]
    public partial class TicketLogPedidoCredito
    {
        [Key]
        public int CodTicketLogPedidoCredito { get; set; }
        public int? CodDespesaPeriodoTecnico { get; set; }
        public double? Valor { get; set; }
        [StringLength(50)]
        public string NumeroCartao { get; set; }
        public int IndProcessado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraProcessamento { get; set; }
        [StringLength(150)]
        public string Observacao { get; set; }
        [Required]
        [StringLength(30)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
