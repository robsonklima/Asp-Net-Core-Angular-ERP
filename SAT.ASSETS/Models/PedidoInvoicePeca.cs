using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoInvoicePeca")]
    public partial class PedidoInvoicePeca
    {
        [Key]
        public int CodPedidoInvoice { get; set; }
        [Key]
        public int CodPedidoPeca { get; set; }
        [StringLength(500)]
        public string DescPeca { get; set; }
        [Required]
        [StringLength(20)]
        public string CodMagnus { get; set; }
        public int QtdPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal TotalNetWeight { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal TotalGrossWeight { get; set; }
        public int? CodPedido { get; set; }
        public int? CodPeca { get; set; }

        [ForeignKey(nameof(CodPedidoInvoice))]
        [InverseProperty(nameof(PedidoInvoice.PedidoInvoicePecas))]
        public virtual PedidoInvoice CodPedidoInvoiceNavigation { get; set; }
        [ForeignKey(nameof(CodPedidoPeca))]
        [InverseProperty(nameof(PedidoPeca.PedidoInvoicePecas))]
        public virtual PedidoPeca CodPedidoPecaNavigation { get; set; }
    }
}
