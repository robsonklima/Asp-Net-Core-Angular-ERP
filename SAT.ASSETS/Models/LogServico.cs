using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LogServico")]
    public partial class LogServico
    {
        [Key]
        [Column("codLogServico")]
        public int CodLogServico { get; set; }
        [Required]
        [Column("nomeServico")]
        [StringLength(500)]
        public string NomeServico { get; set; }
        [Column("dataHoraExcucaoInicio", TypeName = "datetime")]
        public DateTime? DataHoraExcucaoInicio { get; set; }
        [Column("dataHoraExecucaoFim", TypeName = "datetime")]
        public DateTime? DataHoraExecucaoFim { get; set; }
        [Column("observacao")]
        [StringLength(200)]
        public string Observacao { get; set; }
        [Column("indEncerramentoSucesso")]
        public int IndEncerramentoSucesso { get; set; }
        [Column("nomeServidor")]
        [StringLength(50)]
        public string NomeServidor { get; set; }
        [Column("detalhamentoOservacao")]
        [StringLength(8000)]
        public string DetalhamentoOservacao { get; set; }
        public int IndVerificado { get; set; }
    }
}
