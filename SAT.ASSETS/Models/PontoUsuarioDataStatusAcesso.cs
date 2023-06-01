using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataStatusAcesso")]
    public partial class PontoUsuarioDataStatusAcesso
    {
        public PontoUsuarioDataStatusAcesso()
        {
            PontoUsuarioData = new HashSet<PontoUsuarioDatum>();
            PontoUsuarioDataControleAlteracaoAcessos = new HashSet<PontoUsuarioDataControleAlteracaoAcesso>();
        }

        [Key]
        public int CodPontoUsuarioDataStatusAcesso { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(PontoUsuarioDatum.CodPontoUsuarioDataStatusAcessoNavigation))]
        public virtual ICollection<PontoUsuarioDatum> PontoUsuarioData { get; set; }
        [InverseProperty(nameof(PontoUsuarioDataControleAlteracaoAcesso.CodPontoUsuarioDataStatusAcessoNavigation))]
        public virtual ICollection<PontoUsuarioDataControleAlteracaoAcesso> PontoUsuarioDataControleAlteracaoAcessos { get; set; }
    }
}
