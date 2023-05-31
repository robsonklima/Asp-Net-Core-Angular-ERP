using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATFoto")]
    public partial class Ratfoto
    {
        [Key]
        public int CodRatFoto { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [StringLength(50)]
        public string NumRat { get; set; }
        [StringLength(140)]
        public string Url { get; set; }
        [StringLength(90)]
        public string Descricao { get; set; }
    }
}
