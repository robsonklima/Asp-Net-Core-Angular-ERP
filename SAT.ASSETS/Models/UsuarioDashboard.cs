using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("UsuarioDashboard")]
    public partial class UsuarioDashboard
    {
        [Key]
        public int CodUsuarioDashboard { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodDashboard { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }

        [ForeignKey(nameof(CodDashboard))]
        [InverseProperty(nameof(Dashboard.UsuarioDashboards))]
        public virtual Dashboard CodDashboardNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.UsuarioDashboards))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
