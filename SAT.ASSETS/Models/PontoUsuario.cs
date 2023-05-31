using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuario")]
    public partial class PontoUsuario
    {
        [Key]
        public int CodPontoUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraRegistro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraEnvio { get; set; }
        public byte? IndAprovado { get; set; }
        public byte? IndRevisado { get; set; }
        public byte IndAtivo { get; set; }
        public string Observacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioAprov { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [ForeignKey(nameof(CodPontoPeriodo))]
        [InverseProperty(nameof(PontoPeriodo.PontoUsuarios))]
        public virtual PontoPeriodo CodPontoPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.PontoUsuarios))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
