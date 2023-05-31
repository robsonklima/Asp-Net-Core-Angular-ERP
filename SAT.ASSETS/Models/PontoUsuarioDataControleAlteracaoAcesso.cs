using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataControleAlteracaoAcesso")]
    public partial class PontoUsuarioDataControleAlteracaoAcesso
    {
        [Key]
        public int CodPontoUsuarioDataControleAlteracaoAcesso { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int CodPontoUsuarioDataModoAlteracaoAcesso { get; set; }
        public int CodPontoUsuarioDataStatusAcesso { get; set; }
        public int CodPontoUsuarioDataJustificativaAlteracaoAcesso { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodPontoUsuarioDataJustificativaAlteracaoAcesso))]
        [InverseProperty(nameof(PontoUsuarioDataJustificativaAlteracaoAcesso.PontoUsuarioDataControleAlteracaoAcessos))]
        public virtual PontoUsuarioDataJustificativaAlteracaoAcesso CodPontoUsuarioDataJustificativaAlteracaoAcessoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioDataModoAlteracaoAcesso))]
        [InverseProperty(nameof(PontoUsuarioDataModoAlteracaoAcesso.PontoUsuarioDataControleAlteracaoAcessos))]
        public virtual PontoUsuarioDataModoAlteracaoAcesso CodPontoUsuarioDataModoAlteracaoAcessoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioData))]
        [InverseProperty(nameof(PontoUsuarioDatum.PontoUsuarioDataControleAlteracaoAcessos))]
        public virtual PontoUsuarioDatum CodPontoUsuarioDataNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioDataStatusAcesso))]
        [InverseProperty(nameof(PontoUsuarioDataStatusAcesso.PontoUsuarioDataControleAlteracaoAcessos))]
        public virtual PontoUsuarioDataStatusAcesso CodPontoUsuarioDataStatusAcessoNavigation { get; set; }
    }
}
