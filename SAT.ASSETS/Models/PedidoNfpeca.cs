using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoNFPeca")]
    public partial class PedidoNfpeca
    {
        [Key]
        [Column("CodSolicitacaoNF")]
        public int CodSolicitacaoNf { get; set; }
        [Key]
        public int CodPedidoPeca { get; set; }
        public int Qtd { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodPeca { get; set; }
        public int? CodPedido { get; set; }

        [ForeignKey(nameof(CodPeca))]
        [InverseProperty(nameof(Peca.PedidoNfpecas))]
        public virtual Peca CodPecaNavigation { get; set; }
        [ForeignKey(nameof(CodPedido))]
        [InverseProperty(nameof(Pedido.PedidoNfpecas))]
        public virtual Pedido CodPedidoNavigation { get; set; }
        [ForeignKey(nameof(CodPedidoPeca))]
        [InverseProperty(nameof(PedidoPeca.PedidoNfpecas))]
        public virtual PedidoPeca CodPedidoPecaNavigation { get; set; }
        [ForeignKey(nameof(CodSolicitacaoNf))]
        [InverseProperty(nameof(SolicitacaoNf.PedidoNfpecas))]
        public virtual SolicitacaoNf CodSolicitacaoNfNavigation { get; set; }
    }
}
