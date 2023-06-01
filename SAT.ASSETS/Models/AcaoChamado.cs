using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AcaoChamado")]
    public partial class AcaoChamado
    {
        public AcaoChamado()
        {
            ChamadoHists = new HashSet<ChamadoHist>();
        }

        [Key]
        public int CodAcaoChamado { get; set; }
        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string Cor { get; set; }
        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [InverseProperty(nameof(ChamadoHist.CodAcaoChamadoNavigation))]
        public virtual ICollection<ChamadoHist> ChamadoHists { get; set; }
    }
}
