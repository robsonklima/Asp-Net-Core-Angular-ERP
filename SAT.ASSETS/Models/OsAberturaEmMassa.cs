using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OsAberturaEmMassa")]
    public partial class OsAberturaEmMassa
    {
        [Key]
        public int CodOsAberturaEmMassa { get; set; }
        public int? CodOs { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(300)]
        public string Detalhes { get; set; }
    }
}
