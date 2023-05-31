using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Orcamentos")]
    public partial class Orcamento1
    {
        public Orcamento1()
        {
            OrcamentosOutrosServicos = new HashSet<OrcamentosOutrosServico>();
            OrcamentosPecas = new HashSet<OrcamentosPeca>();
        }

        [Key]
        public int CodOrcamento { get; set; }
        public int CodFilial { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int? CodOrcamentoStatus { get; set; }
        public int? CodOrcamentoTipo { get; set; }
        [StringLength(20)]
        public string NumOrcamento { get; set; }
        [StringLength(40)]
        public string DescOrcamentoTipo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorHoraTecnica { get; set; }
        [StringLength(20)]
        public string PrevisaoHoras { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorUnitarioKmRodado { get; set; }
        [Column("KM")]
        [StringLength(10)]
        public string Km { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorHoraDeslocamento { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorTotalOrcamento { get; set; }
        public byte IndAtivo { get; set; }
        public string ObsMotivo { get; set; }
        public string DetalheOrcamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEnvioAprovacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAprovacaoReprovacao { get; set; }
        public byte? IndEnvioAprovaReprova { get; set; }
        public byte? IndEnviadoParaCliente { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioDescong { get; set; }
        [StringLength(10)]
        public string DataHoraDescong { get; set; }

        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.Orcamento1s))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodOrcamentoStatus))]
        [InverseProperty(nameof(OrcamentosStatus.Orcamento1s))]
        public virtual OrcamentosStatus CodOrcamentoStatusNavigation { get; set; }
        [ForeignKey(nameof(CodOrcamentoTipo))]
        [InverseProperty(nameof(OrcamentosTipo.Orcamento1s))]
        public virtual OrcamentosTipo CodOrcamentoTipoNavigation { get; set; }
        [InverseProperty(nameof(OrcamentosOutrosServico.CodOrcamentoNavigation))]
        public virtual ICollection<OrcamentosOutrosServico> OrcamentosOutrosServicos { get; set; }
        [InverseProperty(nameof(OrcamentosPeca.CodOrcamentoNavigation))]
        public virtual ICollection<OrcamentosPeca> OrcamentosPecas { get; set; }
    }
}
