using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ChecklistPreventivaLayout")]
    public partial class ChecklistPreventivaLayout
    {
        public int CodChecklistPreventivaLayout { get; set; }
        [StringLength(50)]
        public string Layout { get; set; }
        public int? IndAtivo { get; set; }
    }
}
