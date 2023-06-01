using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PontoUsuarioDatum
    {
        public PontoUsuarioDatum()
        {
            PontoUsuarioDataAdvertencia = new HashSet<PontoUsuarioDataAdvertencium>();
            PontoUsuarioDataControleAlteracaoAcessos = new HashSet<PontoUsuarioDataControleAlteracaoAcesso>();
            PontoUsuarioDataDivergencia = new HashSet<PontoUsuarioDataDivergencium>();
            PontoUsuarioDataDivergenciaRats = new HashSet<PontoUsuarioDataDivergenciaRat>();
            PontoUsuarioDataValidacaos = new HashSet<PontoUsuarioDataValidacao>();
        }

        [Key]
        public int CodPontoUsuarioData { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        public int CodPontoUsuarioDataStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataRegistro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        public int CodPontoUsuarioDataStatusAcesso { get; set; }

        [ForeignKey(nameof(CodPontoPeriodo))]
        [InverseProperty(nameof(PontoPeriodo.PontoUsuarioData))]
        public virtual PontoPeriodo CodPontoPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioDataStatusAcesso))]
        [InverseProperty(nameof(PontoUsuarioDataStatusAcesso.PontoUsuarioData))]
        public virtual PontoUsuarioDataStatusAcesso CodPontoUsuarioDataStatusAcessoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioDataStatus))]
        [InverseProperty(nameof(PontoUsuarioDataStatus.PontoUsuarioData))]
        public virtual PontoUsuarioDataStatus CodPontoUsuarioDataStatusNavigation { get; set; }
        [InverseProperty(nameof(PontoUsuarioDataAdvertencium.CodPontoUsuarioDataNavigation))]
        public virtual ICollection<PontoUsuarioDataAdvertencium> PontoUsuarioDataAdvertencia { get; set; }
        [InverseProperty(nameof(PontoUsuarioDataControleAlteracaoAcesso.CodPontoUsuarioDataNavigation))]
        public virtual ICollection<PontoUsuarioDataControleAlteracaoAcesso> PontoUsuarioDataControleAlteracaoAcessos { get; set; }
        [InverseProperty(nameof(PontoUsuarioDataDivergencium.CodPontoUsuarioDataNavigation))]
        public virtual ICollection<PontoUsuarioDataDivergencium> PontoUsuarioDataDivergencia { get; set; }
        [InverseProperty(nameof(PontoUsuarioDataDivergenciaRat.CodPontoUsuarioDataNavigation))]
        public virtual ICollection<PontoUsuarioDataDivergenciaRat> PontoUsuarioDataDivergenciaRats { get; set; }
        [InverseProperty(nameof(PontoUsuarioDataValidacao.CodPontoUsuarioDataNavigation))]
        public virtual ICollection<PontoUsuarioDataValidacao> PontoUsuarioDataValidacaos { get; set; }
    }
}
