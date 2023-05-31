using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoRetencao")]
    public partial class TipoRetencao
    {
        public TipoRetencao()
        {
            ContratoEquipRetencaos = new HashSet<ContratoEquipRetencao>();
        }

        [Key]
        public int CodTipoRetencao { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeTipoRetencao { get; set; }
        [StringLength(50)]
        public string DescTipoRetencao { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(ContratoEquipRetencao.CodTipoRetencaoNavigation))]
        public virtual ICollection<ContratoEquipRetencao> ContratoEquipRetencaos { get; set; }
    }
}
