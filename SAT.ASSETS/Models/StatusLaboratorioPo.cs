using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusLaboratorioPOS")]
    public partial class StatusLaboratorioPo
    {
        public StatusLaboratorioPo()
        {
            LaboratorioPos = new HashSet<LaboratorioPo>();
        }

        [Key]
        [Column("CodStatusLaboratorioPOS")]
        public int CodStatusLaboratorioPos { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(50)]
        public string Icon { get; set; }

        [InverseProperty(nameof(LaboratorioPo.CodStatusLaboratorioPosNavigation))]
        public virtual ICollection<LaboratorioPo> LaboratorioPos { get; set; }
    }
}
