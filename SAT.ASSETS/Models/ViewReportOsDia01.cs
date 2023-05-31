using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ViewReportOsDia01
    {
        [Column("codcliente")]
        public int Codcliente { get; set; }
        [StringLength(50)]
        public string NomeCliente { get; set; }
        [Column("totalcliente")]
        public int? Totalcliente { get; set; }
        [Column("tot_os_dia")]
        public int? TotOsDia { get; set; }
    }
}
