using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoHistoricoEquipamentoPOS")]
    public partial class TipoHistoricoEquipamentoPo
    {
        public TipoHistoricoEquipamentoPo()
        {
            EquipamentoPoshists = new HashSet<EquipamentoPoshist>();
        }

        [Key]
        [Column("CodTipoHistoricoEquipamentoPOS")]
        public int CodTipoHistoricoEquipamentoPos { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [InverseProperty(nameof(EquipamentoPoshist.CodTipoHistoricoEquipamentoPosNavigation))]
        public virtual ICollection<EquipamentoPoshist> EquipamentoPoshists { get; set; }
    }
}
