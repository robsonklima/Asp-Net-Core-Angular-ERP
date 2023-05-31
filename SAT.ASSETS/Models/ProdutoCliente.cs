using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ProdutoCliente")]
    public partial class ProdutoCliente
    {
        [Key]
        public int CodProduto { get; set; }
        [Key]
        public int CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string CodProdutoCliente { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.ProdutoClientes))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodProduto))]
        [InverseProperty(nameof(Produto.ProdutoClientes))]
        public virtual Produto CodProdutoNavigation { get; set; }
    }
}
