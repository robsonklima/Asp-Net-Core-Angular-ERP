using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("testeinterop")]
    public partial class Testeinterop
    {
        [Column("teste1")]
        [StringLength(10)]
        public string Teste1 { get; set; }
        [Column("teste2")]
        [StringLength(10)]
        public string Teste2 { get; set; }
    }
}
