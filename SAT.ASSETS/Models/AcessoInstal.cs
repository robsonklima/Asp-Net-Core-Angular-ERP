using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("AcessoInstal")]
    public partial class AcessoInstal
    {
        [Column("codAcessoInstal")]
        public int CodAcessoInstal { get; set; }
        [Required]
        [Column("codigo")]
        [StringLength(50)]
        public string Codigo { get; set; }
        [Column("indTipoAcesso")]
        public int IndTipoAcesso { get; set; }
        [Column("numInstalCampo")]
        public int NumInstalCampo { get; set; }
        [Required]
        [Column("indAcesso")]
        [StringLength(1)]
        public string IndAcesso { get; set; }
        [Column("dtCadastro", TypeName = "datetime")]
        public DateTime DtCadastro { get; set; }
    }
}
