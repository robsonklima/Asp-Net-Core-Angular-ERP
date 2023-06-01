using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentosFaturamentobkp")]
    public partial class OrcamentosFaturamentobkp
    {
        [Key]
        public int CodOrcamentoFaturamento { get; set; }
        public int? CodOrcamento { get; set; }
        public int? CodClienteBancada { get; set; }
        public int CodFilial { get; set; }
        [Column("NumOSPerto")]
        public long NumOsperto { get; set; }
        [StringLength(20)]
        public string NumOrcamento { get; set; }
        [StringLength(150)]
        public string DescricaoNotaFiscal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorPeca { get; set; }
        public int? QtdePeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorServico { get; set; }
        [Column("NumNF")]
        public int NumNf { get; set; }
        [Column("DataEmissaoNF", TypeName = "datetime")]
        public DateTime DataEmissaoNf { get; set; }
        public byte? IndFaturado { get; set; }
        public byte? IndRegistroDanfe { get; set; }
        [StringLength(200)]
        public string CaminhoDanfe { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodClienteBancada))]
        [InverseProperty(nameof(ClienteBancadum.OrcamentosFaturamentobkps))]
        public virtual ClienteBancadum CodClienteBancadaNavigation { get; set; }
        [ForeignKey(nameof(CodOrcamento))]
        [InverseProperty(nameof(Orc.OrcamentosFaturamentobkps))]
        public virtual Orc CodOrcamentoNavigation { get; set; }
    }
}
