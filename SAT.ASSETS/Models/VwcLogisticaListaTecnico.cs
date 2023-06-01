using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogisticaListaTecnico
    {
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Required]
        [StringLength(47)]
        public string Expr1 { get; set; }
        public int? NumFaltante { get; set; }
        [Required]
        [StringLength(40)]
        public string Sinc { get; set; }
    }
}
