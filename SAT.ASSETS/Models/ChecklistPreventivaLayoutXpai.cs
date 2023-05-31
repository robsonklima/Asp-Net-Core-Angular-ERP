using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ChecklistPreventivaLayoutXPai")]
    public partial class ChecklistPreventivaLayoutXpai
    {
        [Column("CodChecklistPreventivaLayoutXPai")]
        public int CodChecklistPreventivaLayoutXpai { get; set; }
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [StringLength(200)]
        public string NomeEquip { get; set; }
        public int? CodChecklistPreventivaLayout { get; set; }
        public int? IndAtivo { get; set; }
    }
}
