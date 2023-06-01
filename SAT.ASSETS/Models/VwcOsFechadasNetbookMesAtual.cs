using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcOsFechadasNetbookMesAtual
    {
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("Nome Técnico")]
        [StringLength(50)]
        public string NomeTécnico { get; set; }
        [Column("Data Hora Fechamento TÉCNICO", TypeName = "datetime")]
        public DateTime? DataHoraFechamentoTécnico { get; set; }
        [Column("Data Hora Fechamento OS", TypeName = "datetime")]
        public DateTime? DataHoraFechamentoOs { get; set; }
    }
}
