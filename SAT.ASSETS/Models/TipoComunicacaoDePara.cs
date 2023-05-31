using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoComunicacaoDePara")]
    public partial class TipoComunicacaoDePara
    {
        [Key]
        public int CodTipoComunicacaoDePara { get; set; }
        public int CodTipoComunicacao { get; set; }
        public int CodCliente { get; set; }
        public int Codigo { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.TipoComunicacaoDeParas))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodTipoComunicacao))]
        [InverseProperty(nameof(TipoComunicacao.TipoComunicacaoDeParas))]
        public virtual TipoComunicacao CodTipoComunicacaoNavigation { get; set; }
    }
}
