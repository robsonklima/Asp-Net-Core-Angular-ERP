using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Dashboard")]
    public partial class Dashboard
    {
        public Dashboard()
        {
            UsuarioDashboards = new HashSet<UsuarioDashboard>();
        }

        [Key]
        public int CodDashboard { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        public string Action { get; set; }
        [Required]
        [StringLength(100)]
        public string Controller { get; set; }
        public bool Partial { get; set; }

        [InverseProperty(nameof(UsuarioDashboard.CodDashboardNavigation))]
        public virtual ICollection<UsuarioDashboard> UsuarioDashboards { get; set; }
    }
}
