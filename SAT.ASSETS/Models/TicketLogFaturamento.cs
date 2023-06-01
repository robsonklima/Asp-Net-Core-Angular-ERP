using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TicketLogFaturamento")]
    public partial class TicketLogFaturamento
    {
        [Key]
        public int CodTicketLogFaturamento { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroCartao { get; set; }
        [Required]
        [Column("descricaoGrupo")]
        [StringLength(100)]
        public string DescricaoGrupo { get; set; }
        [Column("valorCredito")]
        public double ValorCredito { get; set; }
        [Column("valorSaldo")]
        public double ValorSaldo { get; set; }
        [Column("valorTotalCompras")]
        public double ValorTotalCompras { get; set; }
        [Column("valorCreditoFuturo")]
        public double ValorCreditoFuturo { get; set; }
        [Column("dataHoraConsulta", TypeName = "datetime")]
        public DateTime DataHoraConsulta { get; set; }
    }
}
