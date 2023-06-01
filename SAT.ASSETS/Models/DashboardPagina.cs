using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DashboardPagina")]
    public partial class DashboardPagina
    {
        public DashboardPagina()
        {
            DashboardConfiguracaos = new HashSet<DashboardConfiguracao>();
        }

        [Key]
        public int CodPagina { get; set; }
        [Required]
        [StringLength(200)]
        public string NomePagina { get; set; }
        [Column("link")]
        [StringLength(500)]
        public string Link { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
        public int? Ordenacao { get; set; }
        [Column("pathThumb")]
        [StringLength(500)]
        public string PathThumb { get; set; }
        [StringLength(200)]
        public string NomePaginaAlternativa { get; set; }
        [Column("pathThumbAlternativo")]
        [StringLength(500)]
        public string PathThumbAlternativo { get; set; }

        [InverseProperty(nameof(DashboardConfiguracao.CodPaginaNavigation))]
        public virtual ICollection<DashboardConfiguracao> DashboardConfiguracaos { get; set; }
    }
}
