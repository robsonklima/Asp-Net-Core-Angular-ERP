using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ChecklistPreventivaAcompanhamento")]
    public partial class ChecklistPreventivaAcompanhamento
    {
        public int CodChecklistPreventivaAcompanhamento { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Data { get; set; }
        [StringLength(50)]
        public string Contato { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public string Proposta { get; set; }
        public string Obs { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
