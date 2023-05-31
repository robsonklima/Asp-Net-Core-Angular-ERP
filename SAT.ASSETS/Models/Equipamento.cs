using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Equipamento")]
    public partial class Equipamento
    {
        public Equipamento()
        {
            BaseEquipamentoBanrisulItems = new HashSet<BaseEquipamentoBanrisulItem>();
            Chamados = new HashSet<Chamado>();
            EquipamentoCheckListDefeitos = new HashSet<EquipamentoCheckListDefeito>();
            EquipamentoComponentePos = new HashSet<EquipamentoComponentePo>();
            EquipamentoDeParaPos = new HashSet<EquipamentoDeParaPo>();
            EquipamentoDefeitoPos = new HashSet<EquipamentoDefeitoPo>();
            EquipamentoPos = new HashSet<EquipamentoPo>();
            EquipamentoTipoComunicacaos = new HashSet<EquipamentoTipoComunicacao>();
            PatrimonioPos = new HashSet<PatrimonioPo>();
            Produtos = new HashSet<Produto>();
        }

        [Key]
        public int CodEquip { get; set; }
        [Key]
        public int CodGrupoEquip { get; set; }
        [Key]
        public int CodTipoEquip { get; set; }
        [Required]
        [Column("CodEEquip")]
        [StringLength(30)]
        public string CodEequip { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(100)]
        public string DescEquip { get; set; }

        [ForeignKey("CodTipoEquip,CodGrupoEquip")]
        [InverseProperty(nameof(GrupoEquipamento.Equipamentos))]
        public virtual GrupoEquipamento Cod { get; set; }
        [InverseProperty(nameof(BaseEquipamentoBanrisulItem.Cod))]
        public virtual ICollection<BaseEquipamentoBanrisulItem> BaseEquipamentoBanrisulItems { get; set; }
        [InverseProperty(nameof(Chamado.Cod))]
        public virtual ICollection<Chamado> Chamados { get; set; }
        [InverseProperty(nameof(EquipamentoCheckListDefeito.Cod))]
        public virtual ICollection<EquipamentoCheckListDefeito> EquipamentoCheckListDefeitos { get; set; }
        [InverseProperty(nameof(EquipamentoComponentePo.Cod))]
        public virtual ICollection<EquipamentoComponentePo> EquipamentoComponentePos { get; set; }
        [InverseProperty(nameof(EquipamentoDeParaPo.Cod))]
        public virtual ICollection<EquipamentoDeParaPo> EquipamentoDeParaPos { get; set; }
        [InverseProperty(nameof(EquipamentoDefeitoPo.Cod))]
        public virtual ICollection<EquipamentoDefeitoPo> EquipamentoDefeitoPos { get; set; }
        [InverseProperty(nameof(EquipamentoPo.Cod))]
        public virtual ICollection<EquipamentoPo> EquipamentoPos { get; set; }
        [InverseProperty(nameof(EquipamentoTipoComunicacao.Cod))]
        public virtual ICollection<EquipamentoTipoComunicacao> EquipamentoTipoComunicacaos { get; set; }
        [InverseProperty(nameof(PatrimonioPo.Cod))]
        public virtual ICollection<PatrimonioPo> PatrimonioPos { get; set; }
        [InverseProperty(nameof(Produto.Cod))]
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
