using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDespesaItem
    {
        public int? CodFilial { get; set; }
        public int CodTecnico { get; set; }
        public int CodDespesaPeriodo { get; set; }
        public int CodDespesaTipo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DespesaValor { get; set; }
        public int? KmPercorrido { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? KmPrevisto { get; set; }
        public int CodCliente { get; set; }
        public int CodDespesaProtocolo { get; set; }
    }
}
