using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FechamentoManualFaturamento")]
    public partial class FechamentoManualFaturamento
    {
        public int CodCliente { get; set; }
        public int AnoMes { get; set; }
        [Required]
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Column("valor", TypeName = "money")]
        public decimal? Valor { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? CodStatusFaturamento { get; set; }
    }
}
