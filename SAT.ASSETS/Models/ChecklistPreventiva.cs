using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ChecklistPreventiva")]
    public partial class ChecklistPreventiva
    {
        public int CodChecklistPreventiva { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public double? TensaoSemCarga { get; set; }
        public double? TensaoComCarga { get; set; }
        [Column("TensaoEntreTerraENeutro")]
        public double? TensaoEntreTerraEneutro { get; set; }
        public int? RedeEstabilizada { get; set; }
        public int? PossuiNoBreak { get; set; }
        public int? PossuiArCondicionado { get; set; }
        public double? Temperatura { get; set; }
        public string Justificativa { get; set; }
        public int? IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
