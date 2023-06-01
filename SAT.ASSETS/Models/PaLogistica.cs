using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PaLogistica")]
    public partial class PaLogistica
    {
        [Key]
        public int CodPaLogistica { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(250)]
        public string Nome { get; set; }
    }
}
