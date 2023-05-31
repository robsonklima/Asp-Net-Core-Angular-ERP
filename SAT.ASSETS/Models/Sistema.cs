using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Sistema")]
    public partial class Sistema
    {
        public Sistema()
        {
            Menus = new HashSet<Menu>();
            Perfils = new HashSet<Perfil>();
        }

        [Key]
        public int CodSistema { get; set; }
        [StringLength(50)]
        public string NomeSistema { get; set; }
        [StringLength(200)]
        public string DescSistema { get; set; }
        public byte? IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispFim { get; set; }
        public byte? IndDispSabado { get; set; }
        public byte? IndDispDomingo { get; set; }
        public byte? IndDisp24H { get; set; }
        public float? TempoInicio { get; set; }
        public float? TempoReparo { get; set; }
        public float? TempoSolucao { get; set; }
        [StringLength(200)]
        public string MsgSistema { get; set; }

        [InverseProperty(nameof(Menu.CodSistemaNavigation))]
        public virtual ICollection<Menu> Menus { get; set; }
        [InverseProperty(nameof(Perfil.CodSistemaNavigation))]
        public virtual ICollection<Perfil> Perfils { get; set; }
    }
}
