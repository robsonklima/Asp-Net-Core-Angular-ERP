using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistoricoVeiculoCombustivel")]
    public partial class HistoricoVeiculoCombustivel
    {
        [Key]
        public int CodHistoricoVeiculoCombustivel { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraHistorico { get; set; }
        [Required]
        [StringLength(100)]
        public string Tipo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorKm { get; set; }
    }
}
