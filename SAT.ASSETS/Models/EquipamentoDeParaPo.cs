using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoDeParaPOS")]
    public partial class EquipamentoDeParaPo
    {
        [Key]
        [Column("CodEquipamentoDeParaPOS")]
        public int CodEquipamentoDeParaPos { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Column("CodTipoDeParaEquipamentoPOS")]
        public int CodTipoDeParaEquipamentoPos { get; set; }
        [Required]
        [StringLength(50)]
        public string CodDePara { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.EquipamentoDeParaPos))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(CodTipoDeParaEquipamentoPos))]
        [InverseProperty(nameof(TipoDeParaEquipamentoPo.EquipamentoDeParaPos))]
        public virtual TipoDeParaEquipamentoPo CodTipoDeParaEquipamentoPosNavigation { get; set; }
    }
}
