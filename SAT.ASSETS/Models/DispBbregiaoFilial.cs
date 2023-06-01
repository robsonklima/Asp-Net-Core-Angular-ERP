using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBRegiaoFilial")]
    public partial class DispBbregiaoFilial
    {
        [Column("CodDispBBRegiaoFilial")]
        public int CodDispBbregiaoFilial { get; set; }
        [Column("CodDispBBRegiao")]
        [StringLength(10)]
        public string CodDispBbregiao { get; set; }
        public int? CodFilial { get; set; }
        public int? Criticidade { get; set; }
    }
}
