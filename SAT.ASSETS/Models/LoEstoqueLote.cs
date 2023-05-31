using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoEstoqueLote")]
    public partial class LoEstoqueLote
    {
        [Required]
        [Column("cod_empresa")]
        [StringLength(2)]
        public string CodEmpresa { get; set; }
        [Required]
        [Column("cod_item")]
        [StringLength(15)]
        public string CodItem { get; set; }
        [Required]
        [Column("cod_local")]
        [StringLength(10)]
        public string CodLocal { get; set; }
        [Column("num_lote")]
        [StringLength(15)]
        public string NumLote { get; set; }
        [Required]
        [Column("ies_situa_qtd")]
        [StringLength(1)]
        public string IesSituaQtd { get; set; }
        [Column("qtd_saldo", TypeName = "decimal(15, 3)")]
        public decimal QtdSaldo { get; set; }
        [Column("num_transac")]
        public int? NumTransac { get; set; }
    }
}
