using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoComponentePOS")]
    public partial class EquipamentoComponentePo
    {
        [Key]
        [Column("CodEquipComponentePOS")]
        public int CodEquipComponentePos { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Column("CodComponentePOS")]
        public int CodComponentePos { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.EquipamentoComponentePos))]
        public virtual Equipamento Cod { get; set; }
    }
}
