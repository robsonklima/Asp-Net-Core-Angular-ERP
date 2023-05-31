using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ChecklistPreventivaItem")]
    public partial class ChecklistPreventivaItem
    {
        public int CodChecklistPreventivaItem { get; set; }
        public int? CodChecklistPreventiva { get; set; }
        public int? Checado { get; set; }
        public string Obs { get; set; }
        public int? IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? CodChecklistPreventivaLayoutItem { get; set; }
    }
}
