using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Ponto")]
    public partial class Ponto
    {
        [Key]
        public int CodPonto { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPonto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraFim { get; set; }
        public int CodPontoTipoHora { get; set; }
        public byte? IndAprovado { get; set; }
        public byte IndRevisado { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAprov { get; set; }
        [StringLength(300)]
        public string Observacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioAprov { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodPontoPeriodo))]
        [InverseProperty(nameof(PontoPeriodo.Pontos))]
        public virtual PontoPeriodo CodPontoPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoTipoHora))]
        [InverseProperty(nameof(PontoTipoHora.Pontos))]
        public virtual PontoTipoHora CodPontoTipoHoraNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.Pontos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
