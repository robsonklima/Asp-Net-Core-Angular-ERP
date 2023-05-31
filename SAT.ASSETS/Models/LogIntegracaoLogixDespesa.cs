using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LogIntegracaoLogixDespesa")]
    public partial class LogIntegracaoLogixDespesa
    {
        [Key]
        [Column("codIntegracaoLogixDespesa")]
        public int CodIntegracaoLogixDespesa { get; set; }
        [Column("dataHoraCad", TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Column("codDespesaProtocolo")]
        public int? CodDespesaProtocolo { get; set; }
        [StringLength(300)]
        public string Descricao { get; set; }
    }
}
