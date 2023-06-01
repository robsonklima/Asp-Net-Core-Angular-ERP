using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Sequencium
    {
        [Key]
        [Column("TABELA")]
        [StringLength(50)]
        public string Tabela { get; set; }
        [Key]
        [Column("COLUNA")]
        [StringLength(50)]
        public string Coluna { get; set; }
        [Column("CONTADOR")]
        public int? Contador { get; set; }
    }
}
