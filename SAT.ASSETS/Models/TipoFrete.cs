using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoFrete")]
    public partial class TipoFrete
    {
        public TipoFrete()
        {
            Clientes = new HashSet<Cliente>();
            PedidoInvoices = new HashSet<PedidoInvoice>();
            Pedidos = new HashSet<Pedido>();
        }

        [Key]
        public int CodTipoFrete { get; set; }
        [Required]
        [StringLength(3)]
        public string SiglaTipoFrete { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeTipoFrete { get; set; }
        [Required]
        [StringLength(50)]
        public string DescTipoFrete { get; set; }

        [InverseProperty(nameof(Cliente.CodTipoFreteNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [InverseProperty(nameof(PedidoInvoice.CodTipoFreteNavigation))]
        public virtual ICollection<PedidoInvoice> PedidoInvoices { get; set; }
        [InverseProperty(nameof(Pedido.CodTipoFreteNavigation))]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
