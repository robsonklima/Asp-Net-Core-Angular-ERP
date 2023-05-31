using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwmRelDefeitoPosRelVolnei
    {
        public int? Chamados { get; set; }
        public int? Equipamentos { get; set; }
        [Column("Índice_Quebra", TypeName = "decimal(10, 4)")]
        public decimal? ÍndiceQuebra { get; set; }
    }
}
