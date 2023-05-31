using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoComunicacao")]
    public partial class TipoComunicacao
    {
        public TipoComunicacao()
        {
            ChamadoDadosAdicionais = new HashSet<ChamadoDadosAdicionai>();
            EquipamentoTipoComunicacaos = new HashSet<EquipamentoTipoComunicacao>();
            FecharOspos = new HashSet<FecharOspo>();
            Ratbanrisuls = new HashSet<Ratbanrisul>();
            TipoComunicacaoDeParas = new HashSet<TipoComunicacaoDePara>();
        }

        [Key]
        public int CodTipoComunicacao { get; set; }
        [Required]
        [Column("TipoComunicacao")]
        [StringLength(200)]
        public string TipoComunicacao1 { get; set; }
        public bool Ativo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }

        [InverseProperty(nameof(ChamadoDadosAdicionai.CodTipoComunicacaoNavigation))]
        public virtual ICollection<ChamadoDadosAdicionai> ChamadoDadosAdicionais { get; set; }
        [InverseProperty(nameof(EquipamentoTipoComunicacao.CodTipoComunicacaoNavigation))]
        public virtual ICollection<EquipamentoTipoComunicacao> EquipamentoTipoComunicacaos { get; set; }
        [InverseProperty(nameof(FecharOspo.CodTipoComunicacaoNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
        [InverseProperty(nameof(Ratbanrisul.CodTipoComunicacaoNavigation))]
        public virtual ICollection<Ratbanrisul> Ratbanrisuls { get; set; }
        [InverseProperty(nameof(TipoComunicacaoDePara.CodTipoComunicacaoNavigation))]
        public virtual ICollection<TipoComunicacaoDePara> TipoComunicacaoDeParas { get; set; }
    }
}
