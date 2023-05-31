using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DashboardConfiguracao")]
    public partial class DashboardConfiguracao
    {
        [Key]
        public int CodConfig { get; set; }
        public int? CodPagina { get; set; }
        public int? Codperfil { get; set; }
        public int? Codmodalidade { get; set; }
        public int? IndExibe { get; set; }
        public int? IndLinks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DatahoraCad { get; set; }
        [StringLength(100)]
        public string CodusuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DatahoraCadManut { get; set; }
        [StringLength(100)]
        public string CodusuarioCadManut { get; set; }
        public int? Ordenacao { get; set; }

        [ForeignKey(nameof(CodPagina))]
        [InverseProperty(nameof(DashboardPagina.DashboardConfiguracaos))]
        public virtual DashboardPagina CodPaginaNavigation { get; set; }
        [ForeignKey(nameof(Codmodalidade))]
        [InverseProperty(nameof(DashboardModalidade.DashboardConfiguracaos))]
        public virtual DashboardModalidade CodmodalidadeNavigation { get; set; }
        [ForeignKey(nameof(Codperfil))]
        [InverseProperty(nameof(DashboardPerfi.DashboardConfiguracaos))]
        public virtual DashboardPerfi CodperfilNavigation { get; set; }
    }
}
