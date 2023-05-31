using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoEquipamento")]
    public partial class TipoEquipamento
    {
        public TipoEquipamento()
        {
            GrupoEquipamentos = new HashSet<GrupoEquipamento>();
        }

        [Key]
        public int CodTipoEquip { get; set; }
        [Column("CodETipoEquip")]
        [StringLength(2)]
        public string CodEtipoEquip { get; set; }
        [StringLength(50)]
        public string NomeTipoEquip { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(GrupoEquipamento.CodTipoEquipNavigation))]
        public virtual ICollection<GrupoEquipamento> GrupoEquipamentos { get; set; }
    }
}
