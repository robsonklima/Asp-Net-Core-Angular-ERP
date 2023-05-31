using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistDespesaAdiantamento")]
    public partial class HistDespesaAdiantamento
    {
        [Key]
        public int CodHistDespesaAdiantamento { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodDespesaAdiantamentoTipo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAdiantamento { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorAdiantamento { get; set; }
        public byte? IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraExclusao { get; set; }
        [StringLength(20)]
        public string TipoHistorico { get; set; }
    }
}
