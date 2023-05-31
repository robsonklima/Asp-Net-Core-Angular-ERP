using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SlideHE")]
    public partial class SlideHe
    {
        public int? CodFilial { get; set; }
        public int? CodTecnico { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }
        [Column("HE")]
        public int? He { get; set; }
    }
}
