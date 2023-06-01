using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoInvoiceCC")]
    public partial class PedidoInvoiceCc
    {
        [Key]
        [Column("CodPedidoInvoiceCC")]
        public int CodPedidoInvoiceCc { get; set; }
        public int? CodPedidoInvoice { get; set; }
        public int? CodContaCorrente { get; set; }

        [ForeignKey(nameof(CodContaCorrente))]
        [InverseProperty(nameof(ContaCorrente.PedidoInvoiceCcs))]
        public virtual ContaCorrente CodContaCorrenteNavigation { get; set; }
        [ForeignKey(nameof(CodPedidoInvoice))]
        [InverseProperty(nameof(PedidoInvoice.PedidoInvoiceCcs))]
        public virtual PedidoInvoice CodPedidoInvoiceNavigation { get; set; }
    }
}
