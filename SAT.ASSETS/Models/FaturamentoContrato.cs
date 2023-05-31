using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FaturamentoContrato")]
    public partial class FaturamentoContrato
    {
        public int? CodContrato { get; set; }
        [Column("indFaturamentoAutomatico")]
        public int? IndFaturamentoAutomatico { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
