using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("sqlmapoutput")]
    public partial class Sqlmapoutput
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data")]
        [StringLength(4000)]
        public string Data { get; set; }
    }
}
