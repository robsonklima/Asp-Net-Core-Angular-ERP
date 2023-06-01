using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoPeriodo")]
    public partial class PontoPeriodo
    {
        public PontoPeriodo()
        {
            PontoColaboradors = new HashSet<PontoColaborador>();
            PontoMovels = new HashSet<PontoMovel>();
            PontoPeriodoUsuarios = new HashSet<PontoPeriodoUsuario>();
            PontoSobreAvisos = new HashSet<PontoSobreAviso>();
            PontoUsuarioData = new HashSet<PontoUsuarioDatum>();
            PontoUsuarios = new HashSet<PontoUsuario>();
            Pontos = new HashSet<Ponto>();
        }

        [Key]
        public int CodPontoPeriodo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFim { get; set; }
        public int CodPontoPeriodoStatus { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodPontoPeriodoModoAprovacao { get; set; }
        public int CodPontoPeriodoIntervaloAcessoData { get; set; }

        [ForeignKey(nameof(CodPontoPeriodoIntervaloAcessoData))]
        [InverseProperty(nameof(PontoPeriodoIntervaloAcessoDatum.PontoPeriodos))]
        public virtual PontoPeriodoIntervaloAcessoDatum CodPontoPeriodoIntervaloAcessoDataNavigation { get; set; }
        [ForeignKey(nameof(CodPontoPeriodoModoAprovacao))]
        [InverseProperty(nameof(PontoPeriodoModoAprovacao.PontoPeriodos))]
        public virtual PontoPeriodoModoAprovacao CodPontoPeriodoModoAprovacaoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoPeriodoStatus))]
        [InverseProperty(nameof(PontoPeriodoStatus.PontoPeriodos))]
        public virtual PontoPeriodoStatus CodPontoPeriodoStatusNavigation { get; set; }
        [InverseProperty(nameof(PontoColaborador.CodPontoPeriodoNavigation))]
        public virtual ICollection<PontoColaborador> PontoColaboradors { get; set; }
        [InverseProperty(nameof(PontoMovel.CodPontoPeriodoNavigation))]
        public virtual ICollection<PontoMovel> PontoMovels { get; set; }
        [InverseProperty(nameof(PontoPeriodoUsuario.CodPontoPeriodoNavigation))]
        public virtual ICollection<PontoPeriodoUsuario> PontoPeriodoUsuarios { get; set; }
        [InverseProperty(nameof(PontoSobreAviso.CodPontoPeriodoNavigation))]
        public virtual ICollection<PontoSobreAviso> PontoSobreAvisos { get; set; }
        [InverseProperty(nameof(PontoUsuarioDatum.CodPontoPeriodoNavigation))]
        public virtual ICollection<PontoUsuarioDatum> PontoUsuarioData { get; set; }
        [InverseProperty(nameof(PontoUsuario.CodPontoPeriodoNavigation))]
        public virtual ICollection<PontoUsuario> PontoUsuarios { get; set; }
        [InverseProperty(nameof(Ponto.CodPontoPeriodoNavigation))]
        public virtual ICollection<Ponto> Pontos { get; set; }
    }
}
