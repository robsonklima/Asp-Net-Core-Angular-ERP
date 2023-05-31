using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoInvoiceEmbalagem")]
    public partial class PedidoInvoiceEmbalagem
    {
        [Key]
        public int CodPedidoInvoiceEmbalagem { get; set; }
        public int CodPedidoInvoice { get; set; }
        public int CodEmbalagem { get; set; }
        public int? QtdEmbalagem { get; set; }
        [Column("AlturaEmbalagemCM", TypeName = "decimal(10, 2)")]
        public decimal? AlturaEmbalagemCm { get; set; }
        [Column("LarguraEmbalagemCM", TypeName = "decimal(10, 2)")]
        public decimal? LarguraEmbalagemCm { get; set; }
        [Column("ProfundidadeEmbalagemCM", TypeName = "decimal(10, 2)")]
        public decimal? ProfundidadeEmbalagemCm { get; set; }

        [ForeignKey(nameof(CodEmbalagem))]
        [InverseProperty(nameof(Embalagem.PedidoInvoiceEmbalagems))]
        public virtual Embalagem CodEmbalagemNavigation { get; set; }
        [ForeignKey(nameof(CodPedidoInvoice))]
        [InverseProperty(nameof(PedidoInvoice.PedidoInvoiceEmbalagems))]
        public virtual PedidoInvoice CodPedidoInvoiceNavigation { get; set; }
    }
}
