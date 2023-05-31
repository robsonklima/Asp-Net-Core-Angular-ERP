using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaCartaoCombustivelFaturamento")]
    public partial class DespesaCartaoCombustivelFaturamento
    {
        [Key]
        public int CodDespesaCartaoCombustivelFaturamento { get; set; }
        public int CodDespesaCartaoCombustivel { get; set; }
        public double ValorSaldo { get; set; }
        public double ValorCredito { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
