using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataJustificativaAlteracaoAcesso")]
    public partial class PontoUsuarioDataJustificativaAlteracaoAcesso
    {
        public PontoUsuarioDataJustificativaAlteracaoAcesso()
        {
            PontoUsuarioDataControleAlteracaoAcessos = new HashSet<PontoUsuarioDataControleAlteracaoAcesso>();
        }

        [Key]
        public int CodPontoUsuarioDataJustificativaAlteracaoAcesso { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(PontoUsuarioDataControleAlteracaoAcesso.CodPontoUsuarioDataJustificativaAlteracaoAcessoNavigation))]
        public virtual ICollection<PontoUsuarioDataControleAlteracaoAcesso> PontoUsuarioDataControleAlteracaoAcessos { get; set; }
    }
}
