using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CalculoSLA")]
    public partial class CalculoSla
    {
        [Key]
        [Column("CodCalculoSLA")]
        public int CodCalculoSla { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(2)]
        public string SiglaUf { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PercentualDentro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataBaseCalculo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraProcessamento { get; set; }
    }
}
