using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoTipoComunicacao")]
    public partial class EquipamentoTipoComunicacao
    {
        [Key]
        public int CodEquipTipoComunicacao { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodTipoComunicacao { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.EquipamentoTipoComunicacaos))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(CodTipoComunicacao))]
        [InverseProperty(nameof(TipoComunicacao.EquipamentoTipoComunicacaos))]
        public virtual TipoComunicacao CodTipoComunicacaoNavigation { get; set; }
    }
}
