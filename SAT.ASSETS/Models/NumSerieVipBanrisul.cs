using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("NumSerieVipBanrisul")]
    public partial class NumSerieVipBanrisul
    {
        [Key]
        public int CodNumSerieVipBanrisul { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroSerieVip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCadastro { get; set; }
        public bool Ativo { get; set; }
        [StringLength(50)]
        public string CodUsuarioInativacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInativacao { get; set; }
        public int? CodNumSerieVipBanrisulTitulo { get; set; }

        [ForeignKey(nameof(CodNumSerieVipBanrisulTitulo))]
        [InverseProperty(nameof(NumSerieVipBanrisulTitulo.NumSerieVipBanrisuls))]
        public virtual NumSerieVipBanrisulTitulo CodNumSerieVipBanrisulTituloNavigation { get; set; }
    }
}
