using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalGrupoAcesso")]
    public partial class InstalGrupoAcesso
    {
        [Key]
        [Column("codGrupoAcesso")]
        public int CodGrupoAcesso { get; set; }
        [Required]
        [Column("codigo")]
        [StringLength(50)]
        public string Codigo { get; set; }
        [Column("indTipoAcesso")]
        public int IndTipoAcesso { get; set; }
        [Column("indAcessoTodos")]
        [StringLength(50)]
        public string IndAcessoTodos { get; set; }
        [Column("indAcessoImplantacao")]
        [StringLength(50)]
        public string IndAcessoImplantacao { get; set; }
        [Column("indAcessoTradeIn")]
        [StringLength(50)]
        public string IndAcessoTradeIn { get; set; }
        [Column("indAcessoFinanceiro")]
        [StringLength(50)]
        public string IndAcessoFinanceiro { get; set; }
    }
}
