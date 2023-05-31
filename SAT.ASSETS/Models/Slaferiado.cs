using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SLAFeriados")]
    public partial class Slaferiado
    {
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public int? CodFeriado { get; set; }
    }
}
