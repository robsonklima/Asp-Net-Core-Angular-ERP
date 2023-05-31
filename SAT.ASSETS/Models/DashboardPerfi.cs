using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class DashboardPerfi
    {
        public DashboardPerfi()
        {
            DashboardConfiguracaos = new HashSet<DashboardConfiguracao>();
        }

        [Key]
        [Column("codperfil")]
        public int Codperfil { get; set; }
        [Required]
        [Column("nomeperfil")]
        [StringLength(100)]
        public string Nomeperfil { get; set; }
        [Column("indativo")]
        public int? Indativo { get; set; }
        [Column("indExibeLink")]
        public int? IndExibeLink { get; set; }

        [InverseProperty(nameof(DashboardConfiguracao.CodperfilNavigation))]
        public virtual ICollection<DashboardConfiguracao> DashboardConfiguracaos { get; set; }
    }
}
