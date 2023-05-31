using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ChecklistChamadoSTN")]
    public partial class ChecklistChamadoStn
    {
        [Key]
        [Column("CodChecklistChamadoSTN")]
        public int CodChecklistChamadoStn { get; set; }
        [Column("CodCheckListSTN")]
        public int CodCheckListStn { get; set; }
        public int CodAtendimento { get; set; }
        public int CodProtocolo { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        public int CodStatus { get; set; }
        [Column("CodChecklistSTNAlteracao")]
        public int CodChecklistStnalteracao { get; set; }
        [StringLength(50)]
        public string DescCausa { get; set; }
    }
}
