using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoCheckListDefeito")]
    public partial class EquipamentoCheckListDefeito
    {
        [Key]
        public int CodEquipamentoCheckListDefeito { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Column("CodDefeitoPOS")]
        public int CodDefeitoPos { get; set; }
        [Required]
        [StringLength(1000)]
        public string DescricaoItem { get; set; }
        public bool Ativo { get; set; }
        public int? Ordem { get; set; }
        [StringLength(300)]
        public string Referencia { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.EquipamentoCheckListDefeitos))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(CodDefeitoPos))]
        [InverseProperty(nameof(DefeitoPo.EquipamentoCheckListDefeitos))]
        public virtual DefeitoPo CodDefeitoPosNavigation { get; set; }
    }
}
