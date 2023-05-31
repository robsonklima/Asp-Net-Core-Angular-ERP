using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistPonto")]
    public partial class HistPonto
    {
        [Key]
        public int CodHistPonto { get; set; }
        public int CodPonto { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFim { get; set; }
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
        [Column(TypeName = "datetime")]
        public DateTime? DataPonto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHistCad { get; set; }
    }
}
