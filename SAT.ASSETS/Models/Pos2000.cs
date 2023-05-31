using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POS2000")]
    public partial class Pos2000
    {
        [Column("CNPJ")]
        public double? Cnpj { get; set; }
        [Column("Resultado Final", TypeName = "money")]
        public decimal? ResultadoFinal { get; set; }
    }
}
