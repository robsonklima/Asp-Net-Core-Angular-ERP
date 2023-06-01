using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Relatorio")]
    public partial class Relatorio
    {
        public Relatorio()
        {
            RelatorioPerfils = new HashSet<RelatorioPerfil>();
            RelatorioUsuarios = new HashSet<RelatorioUsuario>();
            RelatorioVisaos = new HashSet<RelatorioVisao>();
        }

        [Key]
        public int CodRelatorio { get; set; }
        [Required]
        [StringLength(100)]
        public string NomeRelatorio { get; set; }
        [Required]
        [StringLength(200)]
        public string DescRelatorio { get; set; }
        [Required]
        [StringLength(100)]
        public string DataMemberName { get; set; }
        [Required]
        [StringLength(7)]
        public string CorRelatorio { get; set; }
        public int CodConexao { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodConexao))]
        [InverseProperty(nameof(Conexao.Relatorios))]
        public virtual Conexao CodConexaoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.RelatorioCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.RelatorioCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(RelatorioPerfil.CodRelatorioNavigation))]
        public virtual ICollection<RelatorioPerfil> RelatorioPerfils { get; set; }
        [InverseProperty(nameof(RelatorioUsuario.CodRelatorioNavigation))]
        public virtual ICollection<RelatorioUsuario> RelatorioUsuarios { get; set; }
        [InverseProperty(nameof(RelatorioVisao.CodRelatorioNavigation))]
        public virtual ICollection<RelatorioVisao> RelatorioVisaos { get; set; }
    }
}
