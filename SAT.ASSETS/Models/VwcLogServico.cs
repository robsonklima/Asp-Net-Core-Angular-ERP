using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogServico
    {
        [Column("codLogServico")]
        public int CodLogServico { get; set; }
        [Column("nomeServico")]
        [StringLength(8000)]
        public string NomeServico { get; set; }
        [StringLength(18)]
        public string TempoOciosidade { get; set; }
        [Column("dataHoraInicio", TypeName = "datetime")]
        public DateTime? DataHoraInicio { get; set; }
        [Column("dataHoraExecucaoFim", TypeName = "datetime")]
        public DateTime? DataHoraExecucaoFim { get; set; }
        [StringLength(18)]
        public string Duracao { get; set; }
        [Column("observacao")]
        [StringLength(5016)]
        public string Observacao { get; set; }
        [Column("indEncerramentoSucesso")]
        public int IndEncerramentoSucesso { get; set; }
        [Column("nomeServidor")]
        [StringLength(50)]
        public string NomeServidor { get; set; }
        [Column("indVerificado")]
        public int IndVerificado { get; set; }
    }
}
