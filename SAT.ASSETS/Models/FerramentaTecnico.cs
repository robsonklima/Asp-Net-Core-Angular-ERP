using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FerramentaTecnico")]
    public partial class FerramentaTecnico
    {
        [Key]
        public int CodFerramentaTecnico { get; set; }
        [StringLength(80)]
        public string Nome { get; set; }
        public int? Status { get; set; }
    }
}
