using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Table_1")]
    public partial class Table1
    {
        [Column("sdf")]
        [StringLength(10)]
        public string Sdf { get; set; }
    }
}
