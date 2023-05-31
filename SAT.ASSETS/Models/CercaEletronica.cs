using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CercaEletronica")]
    public partial class CercaEletronica
    {
        [Key]
        public int CodCercaEletronica { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? Liberado { get; set; }
    }
}
