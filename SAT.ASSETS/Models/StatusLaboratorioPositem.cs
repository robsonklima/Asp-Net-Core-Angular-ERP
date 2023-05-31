using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusLaboratorioPOSItem")]
    public partial class StatusLaboratorioPositem
    {
        public StatusLaboratorioPositem()
        {
            LaboratorioPositems = new HashSet<LaboratorioPositem>();
        }

        [Key]
        [Column("CodStatusLaboratorioPOSItem")]
        public int CodStatusLaboratorioPositem { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [InverseProperty(nameof(LaboratorioPositem.CodStatusLaboratorioPositemNavigation))]
        public virtual ICollection<LaboratorioPositem> LaboratorioPositems { get; set; }
    }
}
