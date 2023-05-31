using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("BaseEquipamentoBanrisulItem")]
    public partial class BaseEquipamentoBanrisulItem
    {
        [Key]
        public int Id { get; set; }
        public int IdBaseEquipamentoBanrisul { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        public int Quantidade { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.BaseEquipamentoBanrisulItems))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(IdBaseEquipamentoBanrisul))]
        [InverseProperty(nameof(BaseEquipamentoBanrisul.BaseEquipamentoBanrisulItems))]
        public virtual BaseEquipamentoBanrisul IdBaseEquipamentoBanrisulNavigation { get; set; }
    }
}
