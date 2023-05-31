using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcTecnicoMediaAtend
    {
        [Column("codTecnico")]
        public int CodTecnico { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MediaAtend { get; set; }
    }
}
