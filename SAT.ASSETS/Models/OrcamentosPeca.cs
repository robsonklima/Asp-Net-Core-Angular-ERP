using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentosPeca")]
    public partial class OrcamentosPeca
    {
        [Key]
        public int CodOrcamentoPeca { get; set; }
        public int CodPeca { get; set; }
        public int CodOrcamento { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorUnitarioPecaOrcamento { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorUnitarioPecaOriginal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorDesconto { get; set; }
        public int QtdePeca { get; set; }

        [ForeignKey(nameof(CodOrcamento))]
        [InverseProperty(nameof(Orcamento1.OrcamentosPecas))]
        public virtual Orcamento1 CodOrcamentoNavigation { get; set; }
        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.OrcamentosPecas))]
        public virtual Peca CodPecaNavigation { get; set; }
    }
}
