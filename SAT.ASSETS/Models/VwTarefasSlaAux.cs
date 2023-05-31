using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwTarefasSlaAux
    {
        [Column("codTarefa")]
        public int CodTarefa { get; set; }
        [Column("indLista")]
        public int? IndLista { get; set; }
        [Required]
        [Column("SLA")]
        [StringLength(6)]
        public string Sla { get; set; }
        [Column("dataHoraCadastro", TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
        [Column("dataEncerramento", TypeName = "datetime")]
        public DateTime? DataEncerramento { get; set; }
        [Column("dataHoraFimSLA", TypeName = "datetime")]
        public DateTime? DataHoraFimSla { get; set; }
        [Column("codTarefaStatus")]
        public int CodTarefaStatus { get; set; }
        [StringLength(7)]
        public string Horas { get; set; }
        [Column("indSLA")]
        public int IndSla { get; set; }
    }
}
