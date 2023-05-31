using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("dbadarlan")]
    public partial class Dbadarlan
    {
        [Column("nome")]
        [StringLength(10)]
        public string Nome { get; set; }
        [Column("cod")]
        [StringLength(10)]
        public string Cod { get; set; }
    }
}
