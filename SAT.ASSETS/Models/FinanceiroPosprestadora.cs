using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FinanceiroPOSPrestadora")]
    public partial class FinanceiroPosprestadora
    {
        [Key]
        [Column("CodFinanceiroPOSPrestadora")]
        public int CodFinanceiroPosprestadora { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorVeroSolutechFechado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorVeroSolutechCancelado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorSicrediSolutechFechado { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorSicrediSolutechCancelado { get; set; }
        [Column("ValorVeroCDSAdiantamento", TypeName = "numeric(18, 2)")]
        public decimal? ValorVeroCdsadiantamento { get; set; }
        [Column("ValorVeroCDSSaldoFechado", TypeName = "numeric(18, 2)")]
        public decimal? ValorVeroCdssaldoFechado { get; set; }
        [Column("ValorVeroCDSSaldoCancelado", TypeName = "numeric(18, 2)")]
        public decimal? ValorVeroCdssaldoCancelado { get; set; }
        [Column("ValorSicrediCDSSaldoFechado", TypeName = "numeric(18, 2)")]
        public decimal? ValorSicrediCdssaldoFechado { get; set; }
        [Column("ValorSicrediCDSSaldoCancelado", TypeName = "numeric(18, 2)")]
        public decimal? ValorSicrediCdssaldoCancelado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataBaseMesAno { get; set; }
        public int CodCliente { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.FinanceiroPosprestadoras))]
        public virtual Cliente CodClienteNavigation { get; set; }
    }
}
