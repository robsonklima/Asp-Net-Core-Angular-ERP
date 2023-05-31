using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusEquipamentoPOS")]
    public partial class StatusEquipamentoPo
    {
        public StatusEquipamentoPo()
        {
            EquipamentoPos = new HashSet<EquipamentoPo>();
            EquipamentoPoshistCodStatusEquipamentoPosNavigations = new HashSet<EquipamentoPoshist>();
            EquipamentoPoshistCodStatusEquipamentoPosanteriorNavigations = new HashSet<EquipamentoPoshist>();
        }

        [Key]
        [Column("CodStatusEquipamentoPOS")]
        public int CodStatusEquipamentoPos { get; set; }
        [Required]
        [Column("NomeStatusEquipamentoPOS")]
        [StringLength(100)]
        public string NomeStatusEquipamentoPos { get; set; }
        [Required]
        [Column("CorStatusEquipamentoPOS")]
        [StringLength(50)]
        public string CorStatusEquipamentoPos { get; set; }

        [InverseProperty(nameof(EquipamentoPo.CodStatusEquipamentoPosNavigation))]
        public virtual ICollection<EquipamentoPo> EquipamentoPos { get; set; }
        [InverseProperty(nameof(EquipamentoPoshist.CodStatusEquipamentoPosNavigation))]
        public virtual ICollection<EquipamentoPoshist> EquipamentoPoshistCodStatusEquipamentoPosNavigations { get; set; }
        [InverseProperty(nameof(EquipamentoPoshist.CodStatusEquipamentoPosanteriorNavigation))]
        public virtual ICollection<EquipamentoPoshist> EquipamentoPoshistCodStatusEquipamentoPosanteriorNavigations { get; set; }
    }
}
