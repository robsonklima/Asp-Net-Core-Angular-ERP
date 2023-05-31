using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORLog")]
    public partial class Orlog
    {
        [Key]
        public int CodLog { get; set; }
        [Column("CodORItem")]
        public int CodOritem { get; set; }
        [Column("CodOR")]
        public int CodOr { get; set; }
        [StringLength(100)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraManut { get; set; }
    }
}
