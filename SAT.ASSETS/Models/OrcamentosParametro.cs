using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentosParametro")]
    public partial class OrcamentosParametro
    {
        [Key]
        public int CodOrcamentosParametro { get; set; }
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [StringLength(255)]
        public string Valor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoracad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public byte IndAtivo { get; set; }
    }
}
