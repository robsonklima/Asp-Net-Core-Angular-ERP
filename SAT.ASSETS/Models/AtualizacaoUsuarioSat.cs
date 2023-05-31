using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AtualizacaoUsuarioSAT")]
    public partial class AtualizacaoUsuarioSat
    {
        [Key]
        [Column("CodAtualizacaoUsuarioSAT")]
        public int CodAtualizacaoUsuarioSat { get; set; }
        [Column("CodAtualizacaoSAT")]
        public int CodAtualizacaoSat { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public byte IndAtivo { get; set; }

        [ForeignKey(nameof(CodAtualizacaoSat))]
        [InverseProperty(nameof(AtualizacaoSat.AtualizacaoUsuarioSats))]
        public virtual AtualizacaoSat CodAtualizacaoSatNavigation { get; set; }
    }
}
