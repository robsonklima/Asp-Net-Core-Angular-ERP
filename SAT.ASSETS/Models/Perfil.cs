using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Perfil")]
    public partial class Perfil
    {
        public Perfil()
        {
            PerfilMenus = new HashSet<PerfilMenu>();
            RelatorioPerfils = new HashSet<RelatorioPerfil>();
        }

        [Key]
        public int CodPerfil { get; set; }
        [StringLength(50)]
        public string NomePerfil { get; set; }
        [StringLength(200)]
        public string DescPerfil { get; set; }
        public byte? IndResumo { get; set; }
        public int? CodSistema { get; set; }
        public byte? IndAbreChamado { get; set; }

        [ForeignKey(nameof(CodSistema))]
        [InverseProperty(nameof(Sistema.Perfils))]
        public virtual Sistema CodSistemaNavigation { get; set; }
        [InverseProperty(nameof(PerfilMenu.CodPerfilNavigation))]
        public virtual ICollection<PerfilMenu> PerfilMenus { get; set; }
        [InverseProperty(nameof(RelatorioPerfil.CodPerfilNavigation))]
        public virtual ICollection<RelatorioPerfil> RelatorioPerfils { get; set; }
    }
}
