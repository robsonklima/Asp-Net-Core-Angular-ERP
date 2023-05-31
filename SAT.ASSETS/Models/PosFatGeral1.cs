using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POS_FatGeral")]
    public partial class PosFatGeral1
    {
        [StringLength(255)]
        public string Cliente { get; set; }
        public double? Valor { get; set; }
        [Column("Nota fiscal")]
        [StringLength(255)]
        public string NotaFiscal { get; set; }
        [Column("item")]
        [StringLength(255)]
        public string Item { get; set; }
        [Column("data emissao")]
        [StringLength(255)]
        public string DataEmissao { get; set; }
        [Column("Nome Cliente")]
        [StringLength(255)]
        public string NomeCliente { get; set; }
    }
}
