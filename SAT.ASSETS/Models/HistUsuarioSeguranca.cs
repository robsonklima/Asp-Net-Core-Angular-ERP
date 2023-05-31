using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistUsuarioSeguranca")]
    public partial class HistUsuarioSeguranca
    {
        [Key]
        public int CodHistUsuarioSeguranca { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public byte? SenhaBloqueada { get; set; }
        public byte? SenhaExpirada { get; set; }
        public int QuantidadeTentativaLogin { get; set; }
        public byte? IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
