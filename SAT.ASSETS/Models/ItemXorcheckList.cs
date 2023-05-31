using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ItemXORCheckList")]
    public partial class ItemXorcheckList
    {
        [Key]
        public int CodItemChecklist { get; set; }
        [Column("CodORItem")]
        public int CodOritem { get; set; }
        [Column("CodORChecklist")]
        public int? CodOrchecklist { get; set; }
        [Column("CodORCheckListItem")]
        public int? CodOrcheckListItem { get; set; }
        public int? IndAtivo { get; set; }
        [StringLength(20)]
        public string Nivel { get; set; }
    }
}
