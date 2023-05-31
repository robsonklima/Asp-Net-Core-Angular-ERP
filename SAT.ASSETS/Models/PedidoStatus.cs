using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoStatus")]
    public partial class PedidoStatus
    {
        public PedidoStatus()
        {
            Pedidos = new HashSet<Pedido>();
        }

        [Key]
        public int CodPedidoStatus { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeStatus { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(Pedido.CodPedidoStatusNavigation))]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
