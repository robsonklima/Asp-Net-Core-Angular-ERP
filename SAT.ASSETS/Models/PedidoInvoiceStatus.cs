using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoInvoiceStatus")]
    public partial class PedidoInvoiceStatus
    {
        public PedidoInvoiceStatus()
        {
            PedidoInvoices = new HashSet<PedidoInvoice>();
        }

        [Key]
        public int CodPedidoInvoiceStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeStatus { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(PedidoInvoice.CodPedidoInvoiceStatusNavigation))]
        public virtual ICollection<PedidoInvoice> PedidoInvoices { get; set; }
    }
}
