using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ContaCorrente")]
    public partial class ContaCorrente
    {
        public ContaCorrente()
        {
            PedidoInvoiceCcs = new HashSet<PedidoInvoiceCc>();
        }

        [Key]
        public int CodContaCorrente { get; set; }
        public int? CodCidade { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeBanco { get; set; }
        [StringLength(20)]
        public string Agencia { get; set; }
        [Required]
        [StringLength(20)]
        public string Conta { get; set; }
        [StringLength(50)]
        public string CodigoRapido { get; set; }
        public byte IndAtivo { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.ContaCorrentes))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [InverseProperty(nameof(PedidoInvoiceCc.CodContaCorrenteNavigation))]
        public virtual ICollection<PedidoInvoiceCc> PedidoInvoiceCcs { get; set; }
    }
}
