using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoPeca")]
    public partial class PedidoPeca
    {
        public PedidoPeca()
        {
            PedidoInvoicePecas = new HashSet<PedidoInvoicePeca>();
            PedidoNfpecas = new HashSet<PedidoNfpeca>();
            PedidoPecaObs = new HashSet<PedidoPecaOb>();
            PedidoPecaPcps = new HashSet<PedidoPecaPcp>();
        }

        [Key]
        public int CodPedidoPeca { get; set; }
        public int CodPedido { get; set; }
        public int CodPeca { get; set; }
        public int QtdSolicitada { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Valor { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Desconto { get; set; }
        public int? CodTipoDesconto { get; set; }
        [Column("QtdDiaPrevEntregaPCP")]
        public int? QtdDiaPrevEntregaPcp { get; set; }
        public int? QtdDiaPrevEntregaCliente { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodPedidoPecaStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEntregaProgramada { get; set; }

        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.PedidoPecas))]
        public virtual Peca CodPecaNavigation { get; set; }
        [ForeignKey(nameof(CodPedido))]
        [InverseProperty(nameof(Pedido.PedidoPecas))]
        public virtual Pedido CodPedidoNavigation { get; set; }
        [ForeignKey(nameof(CodPedidoPecaStatus))]
        [InverseProperty(nameof(PedidoPecaStatus.PedidoPecas))]
        public virtual PedidoPecaStatus CodPedidoPecaStatusNavigation { get; set; }
        [ForeignKey(nameof(CodTipoDesconto))]
        [InverseProperty(nameof(TipoDesconto.PedidoPecas))]
        public virtual TipoDesconto CodTipoDescontoNavigation { get; set; }
        [InverseProperty(nameof(PedidoInvoicePeca.CodPedidoPecaNavigation))]
        public virtual ICollection<PedidoInvoicePeca> PedidoInvoicePecas { get; set; }
        [InverseProperty(nameof(PedidoNfpeca.CodPedidoPecaNavigation))]
        public virtual ICollection<PedidoNfpeca> PedidoNfpecas { get; set; }
        [InverseProperty(nameof(PedidoPecaOb.CodPedidoPecaNavigation))]
        public virtual ICollection<PedidoPecaOb> PedidoPecaObs { get; set; }
        [InverseProperty(nameof(PedidoPecaPcp.CodPedidoPecaNavigation))]
        public virtual ICollection<PedidoPecaPcp> PedidoPecaPcps { get; set; }
    }
}
