using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcFaturamento")]
    public partial class OrcFaturamento
    {
        [Key]
        public int CodOrcFaturamento { get; set; }
        public int CodOrc { get; set; }
        [Required]
        [StringLength(3000)]
        public string DescricaoNotaFiscal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorPeca { get; set; }
        public int QuantidadePeca { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal ValorServico { get; set; }
        [Required]
        [StringLength(500)]
        public string NumeroNotaFiscal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEmissaoNotaFiscal { get; set; }
        public byte IsFaturado { get; set; }
        public byte IsRegistroDanfe { get; set; }
        [Required]
        [StringLength(3000)]
        public string CaminhoDanfe { get; set; }
        [Required]
        [StringLength(1000)]
        public string UsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }

        [ForeignKey(nameof(CodOrc))]
        [InverseProperty(nameof(Orc.OrcFaturamentos))]
        public virtual Orc CodOrcNavigation { get; set; }
    }
}
