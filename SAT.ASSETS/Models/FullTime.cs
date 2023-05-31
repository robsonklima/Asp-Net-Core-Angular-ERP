using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FullTime")]
    public partial class FullTime
    {
        [Key]
        [Column("codFullTime")]
        public int CodFullTime { get; set; }
        [StringLength(10)]
        public string CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
        [StringLength(20)]
        public string Janela { get; set; }
        [StringLength(10)]
        public string StatusPrazo { get; set; }
    }
}
