using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PerfilSeguranca")]
    public partial class PerfilSeguranca
    {
        [Key]
        public int CodPerfil { get; set; }
        public int PeriodoAlteracaoSenha { get; set; }
        public int QuantidadeMaximaTentativaLogin { get; set; }
        public byte? SenhaNuncaExpira { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
