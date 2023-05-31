using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusChamado")]
    public partial class StatusChamado
    {
        public StatusChamado()
        {
            Chamados = new HashSet<Chamado>();
        }

        [Key]
        public int CodStatusChamado { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeStatusChamado { get; set; }
        [Required]
        [StringLength(20)]
        public string CorStatusChamado { get; set; }

        [InverseProperty(nameof(Chamado.CodStatusNavigation))]
        public virtual ICollection<Chamado> Chamados { get; set; }
    }
}
