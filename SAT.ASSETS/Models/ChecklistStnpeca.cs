using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ChecklistSTNPecas")]
    public partial class ChecklistStnpeca
    {
        [Key]
        [Column("CodChecklistSTNPecas")]
        public int CodChecklistStnpecas { get; set; }
        [Column("CodCheckListSTN")]
        public int? CodCheckListStn { get; set; }
        public int CodPeca { get; set; }
        public int Quantidade { get; set; }
        public int IndAtivo { get; set; }

        [ForeignKey(nameof(CodCheckListStn))]
        [InverseProperty(nameof(CheckListStn.ChecklistStnpecas))]
        public virtual CheckListStn CodCheckListStnNavigation { get; set; }
    }
}
