using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Performance_ClienteEquipamento")]
    public partial class PerformanceClienteEquipamento
    {
        [Column("CodPerformance_Cliente")]
        public int CodPerformanceCliente { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(20)]
        public string NomeFantasia { get; set; }
        public int? CodEquip { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
    }
}
