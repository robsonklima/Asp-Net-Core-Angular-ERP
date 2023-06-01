using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("GrupoEquipamento")]
    public partial class GrupoEquipamento
    {
        public GrupoEquipamento()
        {
            Equipamentos = new HashSet<Equipamento>();
        }

        [Key]
        public int CodTipoEquip { get; set; }
        [Key]
        public int CodGrupoEquip { get; set; }
        [Column("CodEGrupoEquip")]
        [StringLength(5)]
        public string CodEgrupoEquip { get; set; }
        [StringLength(50)]
        public string NomeGrupoEquip { get; set; }
        public int? CodTraducao { get; set; }

        [ForeignKey(nameof(CodTipoEquip))]
        [InverseProperty(nameof(TipoEquipamento.GrupoEquipamentos))]
        public virtual TipoEquipamento CodTipoEquipNavigation { get; set; }
        [InverseProperty(nameof(Equipamento.Cod))]
        public virtual ICollection<Equipamento> Equipamentos { get; set; }
    }
}
