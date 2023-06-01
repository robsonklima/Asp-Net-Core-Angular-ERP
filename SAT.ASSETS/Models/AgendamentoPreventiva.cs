using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class AgendamentoPreventiva
    {
        [Key]
        public int CodAgendamentoPreventiva { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int Periodo { get; set; }
        public int? IndAtivo { get; set; }
        public int CodEquipContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraUltimaPreventiva { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraPreventivaAgendada { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DatahoraCad { get; set; }
    }
}
