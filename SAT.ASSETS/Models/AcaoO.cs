using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AcaoOS")]
    public partial class AcaoO
    {
        public AcaoO()
        {
            OshistPos = new HashSet<OshistPo>();
        }

        [Key]
        [Column("CodAcaoOS")]
        public int CodAcaoOs { get; set; }
        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string Cor { get; set; }
        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [InverseProperty(nameof(OshistPo.CodAcaoOsNavigation))]
        public virtual ICollection<OshistPo> OshistPos { get; set; }
    }
}
