using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("AnaliseSPLS")]
    public partial class AnaliseSpl
    {
        [Column("NumRAT")]
        [StringLength(255)]
        public string NumRat { get; set; }
        [StringLength(255)]
        public string Ano { get; set; }
    }
}
