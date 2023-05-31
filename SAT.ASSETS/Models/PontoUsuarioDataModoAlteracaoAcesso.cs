using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataModoAlteracaoAcesso")]
    public partial class PontoUsuarioDataModoAlteracaoAcesso
    {
        public PontoUsuarioDataModoAlteracaoAcesso()
        {
            PontoUsuarioDataControleAlteracaoAcessos = new HashSet<PontoUsuarioDataControleAlteracaoAcesso>();
        }

        [Key]
        public int CodPontoUsuarioDataModoAlteracaoAcesso { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(PontoUsuarioDataControleAlteracaoAcesso.CodPontoUsuarioDataModoAlteracaoAcessoNavigation))]
        public virtual ICollection<PontoUsuarioDataControleAlteracaoAcesso> PontoUsuarioDataControleAlteracaoAcessos { get; set; }
    }
}
