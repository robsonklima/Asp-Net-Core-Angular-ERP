using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DashboardModalidade")]
    public partial class DashboardModalidade
    {
        public DashboardModalidade()
        {
            DashboardConfiguracaos = new HashSet<DashboardConfiguracao>();
        }

        [Key]
        public int CodModalidade { get; set; }
        [Required]
        [StringLength(250)]
        public string DescModalidade { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
        [Column("indExibeLink")]
        public int? IndExibeLink { get; set; }

        [InverseProperty(nameof(DashboardConfiguracao.CodmodalidadeNavigation))]
        public virtual ICollection<DashboardConfiguracao> DashboardConfiguracaos { get; set; }
    }
}
