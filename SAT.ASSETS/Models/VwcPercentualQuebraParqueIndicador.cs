using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPercentualQuebraParqueIndicador
    {
        public int CodCliente { get; set; }
        public int? ParqueMaquinasAtivas { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercentualQuebra { get; set; }
    }
}
