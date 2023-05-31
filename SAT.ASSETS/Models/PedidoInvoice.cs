using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoInvoice")]
    public partial class PedidoInvoice
    {
        public PedidoInvoice()
        {
            PedidoInvoiceCcs = new HashSet<PedidoInvoiceCc>();
            PedidoInvoiceEmbalagems = new HashSet<PedidoInvoiceEmbalagem>();
            PedidoInvoicePecas = new HashSet<PedidoInvoicePeca>();
        }

        [Key]
        public int CodPedidoInvoice { get; set; }
        public int? CodPedidoInvoiceStatus { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioResponsavel { get; set; }
        public int CodTipoFrete { get; set; }
        public int? CodClienteDelivery { get; set; }
        [Required]
        [StringLength(200)]
        public string DescInvoiceTo { get; set; }
        [StringLength(200)]
        public string DescDeliveryTo { get; set; }
        [StringLength(200)]
        public string DescAttInvoice { get; set; }
        [StringLength(200)]
        public string DescAttDelivery { get; set; }
        [StringLength(500)]
        public string DescCovering { get; set; }
        [Required]
        [StringLength(50)]
        public string DescMoeda { get; set; }
        [StringLength(200)]
        public string DescOrder { get; set; }
        [Required]
        [StringLength(200)]
        public string DescCountryOrigin { get; set; }
        [StringLength(500)]
        public string DescPrazo { get; set; }
        [StringLength(500)]
        public string DescCreditCard { get; set; }
        [StringLength(500)]
        public string DescCreditLetter { get; set; }
        [StringLength(500)]
        public string DescFrete { get; set; }
        [Required]
        [StringLength(500)]
        public string DescIncoterms { get; set; }
        [StringLength(500)]
        public string DescTransfer { get; set; }
        [StringLength(500)]
        public string DescSeguro { get; set; }
        [StringLength(500)]
        public string DescSigned { get; set; }
        [Column(TypeName = "text")]
        public string DescNotes { get; set; }
        [StringLength(500)]
        public string DescFooter { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorSeguro { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorFrete { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodClienteDelivery))]
        [InverseProperty(nameof(Cliente.PedidoInvoices))]
        public virtual Cliente CodClienteDeliveryNavigation { get; set; }
        [ForeignKey(nameof(CodPedidoInvoiceStatus))]
        [InverseProperty(nameof(PedidoInvoiceStatus.PedidoInvoices))]
        public virtual PedidoInvoiceStatus CodPedidoInvoiceStatusNavigation { get; set; }
        [ForeignKey(nameof(CodTipoFrete))]
        [InverseProperty(nameof(TipoFrete.PedidoInvoices))]
        public virtual TipoFrete CodTipoFreteNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioResponsavel))]
        [InverseProperty(nameof(Usuario.PedidoInvoices))]
        public virtual Usuario CodUsuarioResponsavelNavigation { get; set; }
        [InverseProperty(nameof(PedidoInvoiceCc.CodPedidoInvoiceNavigation))]
        public virtual ICollection<PedidoInvoiceCc> PedidoInvoiceCcs { get; set; }
        [InverseProperty(nameof(PedidoInvoiceEmbalagem.CodPedidoInvoiceNavigation))]
        public virtual ICollection<PedidoInvoiceEmbalagem> PedidoInvoiceEmbalagems { get; set; }
        [InverseProperty(nameof(PedidoInvoicePeca.CodPedidoInvoiceNavigation))]
        public virtual ICollection<PedidoInvoicePeca> PedidoInvoicePecas { get; set; }
    }
}
