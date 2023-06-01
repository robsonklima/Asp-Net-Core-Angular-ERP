using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ViewReportOsDia03
    {
        [Column("codcliente")]
        public int Codcliente { get; set; }
        [Column("nomecliente")]
        [StringLength(50)]
        public string Nomecliente { get; set; }
        [Column("totalcliente")]
        public int? Totalcliente { get; set; }
        [Column("tot_os_dia")]
        public int? TotOsDia { get; set; }
        [Column("codfilial")]
        public int? Codfilial { get; set; }
        [Column("nomefilial")]
        [StringLength(50)]
        public string Nomefilial { get; set; }
        [Column("total_cliente_filial_dia")]
        public int? TotalClienteFilialDia { get; set; }
    }
}
