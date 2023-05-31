using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistOS")]
    public partial class HistO
    {
        [Key]
        [Column("CodHistOS")]
        public int CodHistOs { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public int? CodStatusServico { get; set; }
        public int? CodPosto { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodTecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(50)]
        public string CodUsuarioManutencao { get; set; }
        public int? CodAutorizada { get; set; }
    }
}
