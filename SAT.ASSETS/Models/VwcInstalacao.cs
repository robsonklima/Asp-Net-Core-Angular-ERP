using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcInstalacao
    {
        public int CodInstalacao { get; set; }
        public int CodCliente { get; set; }
        public int? CodInstalLote { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExpedicao { get; set; }
        [Column("DataHoraChegTranspBT", TypeName = "datetime")]
        public DateTime? DataHoraChegTranspBt { get; set; }
        [Column("DataBI", TypeName = "datetime")]
        public DateTime? DataBi { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VlrParcelaEntrega { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VlrParcelaInstalacao { get; set; }
        public int? QtdAtendimentos { get; set; }
        public int? QtdHoras { get; set; }
    }
}
