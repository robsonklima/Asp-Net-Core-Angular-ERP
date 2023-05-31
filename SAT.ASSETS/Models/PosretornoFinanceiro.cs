using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POSRetornoFinanceiro")]
    public partial class PosretornoFinanceiro
    {
        [StringLength(255)]
        public string Tipo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Data { get; set; }
        [Column("CNPJ")]
        public double? Cnpj { get; set; }
        public double? Estab { get; set; }
        public double? Valor { get; set; }
        [StringLength(255)]
        public string F6 { get; set; }
    }
}
