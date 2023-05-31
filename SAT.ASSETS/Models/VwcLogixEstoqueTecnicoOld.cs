using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogixEstoqueTecnicoOld
    {
        public int CodTecnico { get; set; }
        public int? CodPeca { get; set; }
        [Column("QTD", TypeName = "decimal(38, 2)")]
        public decimal? Qtd { get; set; }
        public int? CodFilial { get; set; }
    }
}
