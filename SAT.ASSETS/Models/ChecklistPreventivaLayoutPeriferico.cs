using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ChecklistPreventivaLayoutPeriferico")]
    public partial class ChecklistPreventivaLayoutPeriferico
    {
        public int CodChecklistPreventivaLayoutPeriferico { get; set; }
        public int? CodChecklistPreventivaLayout { get; set; }
        [StringLength(200)]
        public string Periferico { get; set; }
        public int? IndAtivo { get; set; }
    }
}
