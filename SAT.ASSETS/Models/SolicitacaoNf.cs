using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SolicitacaoNF")]
    public partial class SolicitacaoNf
    {
        public SolicitacaoNf()
        {
            PedidoNfpecas = new HashSet<PedidoNfpeca>();
        }

        [Key]
        [Column("CodSolicitacaoNF")]
        public int CodSolicitacaoNf { get; set; }
        public int? CodTransportadora { get; set; }
        public int? CodCliente { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PesoLiquido { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PesoBruto { get; set; }
        [Column("NF")]
        [StringLength(20)]
        public string Nf { get; set; }
        [Column("DataNF", TypeName = "datetime")]
        public DateTime? DataNf { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        public int? Volume { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public byte? IndEmitida { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.SolicitacaoNfs))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodTransportadora))]
        [InverseProperty(nameof(Transportadora.SolicitacaoNfs))]
        public virtual Transportadora CodTransportadoraNavigation { get; set; }
        [InverseProperty(nameof(PedidoNfpeca.CodSolicitacaoNfNavigation))]
        public virtual ICollection<PedidoNfpeca> PedidoNfpecas { get; set; }
    }
}
