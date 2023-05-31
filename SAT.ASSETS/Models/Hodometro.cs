using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Hodometro")]
    public partial class Hodometro
    {
        [StringLength(50)]
        public string Placa { get; set; }
        public double? Valor { get; set; }
        [StringLength(50)]
        public string TipoUso { get; set; }
    }
}
