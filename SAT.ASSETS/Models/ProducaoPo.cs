using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ProducaoPOS")]
    public partial class ProducaoPo
    {
        public ProducaoPo()
        {
            ProducaoPositems = new HashSet<ProducaoPositem>();
        }

        [Key]
        [Column("CodProducaoPOS")]
        public int CodProducaoPos { get; set; }
        [Column("OP")]
        public int Op { get; set; }
        [Required]
        [StringLength(50)]
        public string NotaFiscal { get; set; }
        [Required]
        [StringLength(50)]
        public string Serie { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEmissao { get; set; }
        [StringLength(50)]
        public string NumeroLogicoInicio { get; set; }
        [StringLength(50)]
        public string NumeroLogicoFinal { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column("CodStatusProducaoPOS")]
        public int CodStatusProducaoPos { get; set; }
        [StringLength(1000)]
        public string Erro { get; set; }
        [Column("NumeroOSCliente")]
        [StringLength(50)]
        public string NumeroOscliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Destino { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }
        public int? CodProduto { get; set; }
        [StringLength(50)]
        public string Versao { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorUnitario { get; set; }

        [ForeignKey(nameof(CodStatusProducaoPos))]
        [InverseProperty(nameof(StatusProducaoPo.ProducaoPos))]
        public virtual StatusProducaoPo CodStatusProducaoPosNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.ProducaoPos))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [InverseProperty(nameof(ProducaoPositem.CodProducaoPosNavigation))]
        public virtual ICollection<ProducaoPositem> ProducaoPositems { get; set; }
    }
}
