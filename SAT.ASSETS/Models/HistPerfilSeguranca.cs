using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistPerfilSeguranca")]
    public partial class HistPerfilSeguranca
    {
        [Key]
        public int CodHistPerfilSeguranca { get; set; }
        public int CodPerfil { get; set; }
        public int? PeriodoAlteracaoSenha { get; set; }
        public int? QuantidadeMaximaTentativaLogin { get; set; }
        public byte? SenhaNuncaExpira { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
