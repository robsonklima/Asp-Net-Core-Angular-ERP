using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoMidiaPOS")]
    public partial class TipoMidiaPo
    {
        public TipoMidiaPo()
        {
            EquipamentoPos = new HashSet<EquipamentoPo>();
        }

        [Key]
        [Column("CodTipoMidiaPOS")]
        public int CodTipoMidiaPos { get; set; }
        [Required]
        [Column("NomeTipoMidiaPOS")]
        [StringLength(50)]
        public string NomeTipoMidiaPos { get; set; }

        [InverseProperty(nameof(EquipamentoPo.CodTipoMidiaNavigation))]
        public virtual ICollection<EquipamentoPo> EquipamentoPos { get; set; }
    }
}
