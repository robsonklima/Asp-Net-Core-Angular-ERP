using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoDeParaEquipamentoPOS")]
    public partial class TipoDeParaEquipamentoPo
    {
        public TipoDeParaEquipamentoPo()
        {
            EquipamentoDeParaPos = new HashSet<EquipamentoDeParaPo>();
        }

        [Key]
        [Column("CodTipoDeParaEquipamentoPOS")]
        public int CodTipoDeParaEquipamentoPos { get; set; }
        [Required]
        [Column("NomeTipoDeParaEquipamentoPOS")]
        [StringLength(100)]
        public string NomeTipoDeParaEquipamentoPos { get; set; }

        [InverseProperty(nameof(EquipamentoDeParaPo.CodTipoDeParaEquipamentoPosNavigation))]
        public virtual ICollection<EquipamentoDeParaPo> EquipamentoDeParaPos { get; set; }
    }
}
