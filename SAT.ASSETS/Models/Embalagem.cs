using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Embalagem")]
    public partial class Embalagem
    {
        public Embalagem()
        {
            PedidoInvoiceEmbalagems = new HashSet<PedidoInvoiceEmbalagem>();
        }

        [Key]
        public int CodEmbalagem { get; set; }
        [StringLength(50)]
        public string NomeEmbalagem { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(PedidoInvoiceEmbalagem.CodEmbalagemNavigation))]
        public virtual ICollection<PedidoInvoiceEmbalagem> PedidoInvoiceEmbalagems { get; set; }
    }
}
