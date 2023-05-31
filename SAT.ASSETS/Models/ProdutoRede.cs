using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ProdutoRede")]
    public partial class ProdutoRede
    {
        [Key]
        public int CodProdutoRede { get; set; }
        public int CodProduto { get; set; }
        [Required]
        [StringLength(50)]
        public string CodRede { get; set; }

        [ForeignKey(nameof(CodProduto))]
        [InverseProperty(nameof(Produto.ProdutoRedes))]
        public virtual Produto CodProdutoNavigation { get; set; }
    }
}
