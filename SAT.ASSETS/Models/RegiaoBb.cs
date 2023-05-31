using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("RegiaoBB")]
    public partial class RegiaoBb
    {
        [Column("CodRegiaoBB")]
        public int CodRegiaoBb { get; set; }
        [Column("NomeRegiaoBB")]
        [StringLength(50)]
        public string NomeRegiaoBb { get; set; }
    }
}
