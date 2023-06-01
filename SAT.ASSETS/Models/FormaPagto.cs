using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FormaPagto")]
    public partial class FormaPagto
    {
        public FormaPagto()
        {
            Clientes = new HashSet<Cliente>();
            Pedidos = new HashSet<Pedido>();
        }

        [Key]
        public int CodFormaPagto { get; set; }
        [StringLength(50)]
        public string DescFormaPagto { get; set; }
        public byte? IndAtivo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercAjuste { get; set; }

        [InverseProperty(nameof(Cliente.CodFormaPagtoNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [InverseProperty(nameof(Pedido.CodFormaPagtoNavigation))]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
