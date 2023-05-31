using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ChecklistSTNAlteracao")]
    public partial class ChecklistStnalteracao
    {
        [Key]
        [Column("CodChecklistSTNAlteracao")]
        public int CodChecklistStnalteracao { get; set; }
        [Column("CodCheckListSTN")]
        public int? CodCheckListStn { get; set; }
        public string DescricaoAlteracao { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(50)]
        public string Revisao { get; set; }

        [ForeignKey(nameof(CodCheckListStn))]
        [InverseProperty(nameof(CheckListStn.ChecklistStnalteracaos))]
        public virtual CheckListStn CodCheckListStnNavigation { get; set; }
    }
}
