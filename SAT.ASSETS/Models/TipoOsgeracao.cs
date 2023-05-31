using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoOSGeracao")]
    public partial class TipoOsgeracao
    {
        public TipoOsgeracao()
        {
            ChamadoOs = new HashSet<ChamadoO>();
        }

        [Key]
        [Column("CodTipoOSGeracao")]
        public int CodTipoOsgeracao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipoOsGeracao { get; set; }

        [InverseProperty(nameof(ChamadoO.CodTipoOsGeracaoNavigation))]
        public virtual ICollection<ChamadoO> ChamadoOs { get; set; }
    }
}
