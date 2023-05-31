using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ViewReportOsDia02
    {
        [Column("codcliente")]
        public int Codcliente { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string NomeCliente { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("total_cliente_filial_dia")]
        public int? TotalClienteFilialDia { get; set; }
    }
}
