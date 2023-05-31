using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoDefeitoPOS")]
    public partial class EquipamentoDefeitoPo
    {
        [Key]
        [Column("CodEquipamentoDefeitoPOS")]
        public int CodEquipamentoDefeitoPos { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Column("CodDefeitoPOS")]
        public int CodDefeitoPos { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.EquipamentoDefeitoPos))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(CodDefeitoPos))]
        [InverseProperty(nameof(DefeitoPo.EquipamentoDefeitoPos))]
        public virtual DefeitoPo CodDefeitoPosNavigation { get; set; }
    }
}
