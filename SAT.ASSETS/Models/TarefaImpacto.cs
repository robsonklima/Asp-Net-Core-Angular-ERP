using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TarefaImpacto")]
    public partial class TarefaImpacto
    {
        [Key]
        [Column("codTarefaImpacto")]
        public int CodTarefaImpacto { get; set; }
        [StringLength(5)]
        public string PorcentImpacto { get; set; }
        public int? TempoDesenv { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}
