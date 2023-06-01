using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Orc")]
    public partial class Orc
    {
        public Orc()
        {
            OrcDescontos = new HashSet<OrcDesconto>();
            OrcDeslocamentos = new HashSet<OrcDeslocamento>();
            OrcEnderecos = new HashSet<OrcEndereco>();
            OrcFaturamentos = new HashSet<OrcFaturamento>();
            OrcMaoObras = new HashSet<OrcMaoObra>();
            OrcMaterials = new HashSet<OrcMaterial>();
            OrcOutroServicos = new HashSet<OrcOutroServico>();
            OrcamentosFaturamentobkps = new HashSet<OrcamentosFaturamentobkp>();
        }

        [Key]
        public int CodOrc { get; set; }
        public int? CodigoMotivo { get; set; }
        public int? CodigoStatus { get; set; }
        public int? CodigoSla { get; set; }
        public int? CodigoEquipamento { get; set; }
        public int? CodigoCliente { get; set; }
        public int? CodigoPosto { get; set; }
        public int? CodigoFilial { get; set; }
        public int? CodigoContrato { get; set; }
        public byte? IsMaterialEspecifico { get; set; }
        public int? CodigoOrdemServico { get; set; }
        public int? CodigoEquipamentoContrato { get; set; }
        [StringLength(5000)]
        public string DescricaoOutroMotivo { get; set; }
        [StringLength(5000)]
        public string Detalhe { get; set; }
        [StringLength(5000)]
        public string NomeContrato { get; set; }
        [StringLength(20)]
        public string Numero { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Data { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorIss { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotalDesconto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string UsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEnvioAprovacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAprovacaoCliente { get; set; }

        [InverseProperty(nameof(OrcDesconto.CodOrcNavigation))]
        public virtual ICollection<OrcDesconto> OrcDescontos { get; set; }
        [InverseProperty(nameof(OrcDeslocamento.CodOrcNavigation))]
        public virtual ICollection<OrcDeslocamento> OrcDeslocamentos { get; set; }
        [InverseProperty(nameof(OrcEndereco.CodOrcNavigation))]
        public virtual ICollection<OrcEndereco> OrcEnderecos { get; set; }
        [InverseProperty(nameof(OrcFaturamento.CodOrcNavigation))]
        public virtual ICollection<OrcFaturamento> OrcFaturamentos { get; set; }
        [InverseProperty(nameof(OrcMaoObra.CodOrcNavigation))]
        public virtual ICollection<OrcMaoObra> OrcMaoObras { get; set; }
        [InverseProperty(nameof(OrcMaterial.CodOrcNavigation))]
        public virtual ICollection<OrcMaterial> OrcMaterials { get; set; }
        [InverseProperty(nameof(OrcOutroServico.CodOrcNavigation))]
        public virtual ICollection<OrcOutroServico> OrcOutroServicos { get; set; }
        [InverseProperty(nameof(OrcamentosFaturamentobkp.CodOrcamentoNavigation))]
        public virtual ICollection<OrcamentosFaturamentobkp> OrcamentosFaturamentobkps { get; set; }
    }
}
