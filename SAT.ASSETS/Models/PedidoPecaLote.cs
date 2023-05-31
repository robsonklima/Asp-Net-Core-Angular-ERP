using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoPecaLote")]
    public partial class PedidoPecaLote
    {
        [Key]
        public int CodPedidoPecaLote { get; set; }
        public int CodPedido { get; set; }
        public int CodPeca { get; set; }
        public int QtdPecaLote { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEntregaProgramada { get; set; }
        [StringLength(100)]
        public string ObservacaoLote { get; set; }

        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.PedidoPecaLotes))]
        public virtual Peca CodPecaNavigation { get; set; }
        [ForeignKey(nameof(CodPedido))]
        [InverseProperty(nameof(Pedido.PedidoPecaLotes))]
        public virtual Pedido CodPedidoNavigation { get; set; }
    }
}
