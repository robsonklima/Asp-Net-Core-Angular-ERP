using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ProducaoPOSItem")]
    public partial class ProducaoPositem
    {
        [Key]
        [Column("CodProducaoPOSItem")]
        public int CodProducaoPositem { get; set; }
        [Column("CodProducaoPOS")]
        public int CodProducaoPos { get; set; }
        public long NumeroSerie { get; set; }
        [StringLength(50)]
        public string NumeroLogico { get; set; }
        public int CodProduto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }
        [Column("CodStatusProducaoPOSItem")]
        public int CodStatusProducaoPositem { get; set; }
        [StringLength(1000)]
        public string Erro { get; set; }

        [ForeignKey(nameof(CodProducaoPos))]
        [InverseProperty(nameof(ProducaoPo.ProducaoPositems))]
        public virtual ProducaoPo CodProducaoPosNavigation { get; set; }
        [ForeignKey(nameof(CodProduto))]
        [InverseProperty(nameof(Produto.ProducaoPositems))]
        public virtual Produto CodProdutoNavigation { get; set; }
        [ForeignKey(nameof(CodStatusProducaoPositem))]
        [InverseProperty(nameof(StatusProducaoPositem.ProducaoPositems))]
        public virtual StatusProducaoPositem CodStatusProducaoPositemNavigation { get; set; }
    }
}
