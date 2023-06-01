using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Pedido")]
    public partial class Pedido
    {
        public Pedido()
        {
            PedidoNfpecas = new HashSet<PedidoNfpeca>();
            PedidoPecaLotes = new HashSet<PedidoPecaLote>();
            PedidoPecas = new HashSet<PedidoPeca>();
        }

        [Key]
        public int CodPedido { get; set; }
        public int CodCliente { get; set; }
        public int CodPedidoStatus { get; set; }
        public int? CodFormaPagto { get; set; }
        public int? CodTipoFrete { get; set; }
        [StringLength(300)]
        public string Observacao { get; set; }
        [StringLength(500)]
        public string ObservacaoFinanceira { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercAjuste { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPedidoConfirmacao { get; set; }
        [StringLength(50)]
        public string NumPedidoLogix { get; set; }
        [StringLength(20)]
        public string NumOrdemCompra { get; set; }
        [Column("PercICMS", TypeName = "decimal(10, 2)")]
        public decimal? PercIcms { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraConfirmacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutCliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManutCliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataValidade { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataOrcamento { get; set; }

        [ForeignKey(nameof(CodFormaPagto))]
        [InverseProperty(nameof(FormaPagto.Pedidos))]
        public virtual FormaPagto CodFormaPagtoNavigation { get; set; }
        [ForeignKey(nameof(CodPedidoStatus))]
        [InverseProperty(nameof(PedidoStatus.Pedidos))]
        public virtual PedidoStatus CodPedidoStatusNavigation { get; set; }
        [ForeignKey(nameof(CodTipoFrete))]
        [InverseProperty(nameof(TipoFrete.Pedidos))]
        public virtual TipoFrete CodTipoFreteNavigation { get; set; }
        [InverseProperty(nameof(PedidoNfpeca.CodPedidoNavigation))]
        public virtual ICollection<PedidoNfpeca> PedidoNfpecas { get; set; }
        [InverseProperty(nameof(PedidoPecaLote.CodPedidoNavigation))]
        public virtual ICollection<PedidoPecaLote> PedidoPecaLotes { get; set; }
        [InverseProperty(nameof(PedidoPeca.CodPedidoNavigation))]
        public virtual ICollection<PedidoPeca> PedidoPecas { get; set; }
    }
}
