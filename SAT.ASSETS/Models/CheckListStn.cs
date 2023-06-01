using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CheckListSTN")]
    public partial class CheckListStn
    {
        public CheckListStn()
        {
            ChecklistStnalteracaos = new HashSet<ChecklistStnalteracao>();
            ChecklistStnpecas = new HashSet<ChecklistStnpeca>();
        }

        [Key]
        [Column("CodCheckListSTN")]
        public int CodCheckListStn { get; set; }
        public int CodEquip { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(50)]
        public string Revisao { get; set; }

        [InverseProperty(nameof(ChecklistStnalteracao.CodCheckListStnNavigation))]
        public virtual ICollection<ChecklistStnalteracao> ChecklistStnalteracaos { get; set; }
        [InverseProperty(nameof(ChecklistStnpeca.CodCheckListStnNavigation))]
        public virtual ICollection<ChecklistStnpeca> ChecklistStnpecas { get; set; }
    }
}
