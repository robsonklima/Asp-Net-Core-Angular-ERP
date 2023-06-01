using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_Cliente")]
    public partial class PerformanceCliente
    {
        [Column("CodPerformance_Cliente")]
        public int CodPerformanceCliente { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(20)]
        public string NomeFantasia { get; set; }
    }
}
