using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FecharOSPOS")]
    public partial class FecharOspo
    {
        public FecharOspo()
        {
            FecharOspositens = new HashSet<FecharOspositen>();
        }

        [Key]
        [Column("CodFecharOSPOS")]
        public int CodFecharOspos { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodStatusServico { get; set; }
        public int CodAutorizada { get; set; }
        public int CodTecnico { get; set; }
        [Required]
        [StringLength(50)]
        public string Acompanhante { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InicioAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FimAtendimento { get; set; }
        public int? CodTipoComunicacao { get; set; }
        [Required]
        [StringLength(15)]
        public string Rede { get; set; }
        [Column("CodDefeitoPOS")]
        public int? CodDefeitoPos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataRecebimento { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioEnvio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataIntegracao { get; set; }
        public bool? Erro { get; set; }
        [StringLength(2000)]
        public string ErroIntegracao { get; set; }
        public bool? Processado { get; set; }
        public int? CodMotivoCancelamento { get; set; }
        [StringLength(1000)]
        public string MotivoCancelamento { get; set; }
        [StringLength(1000)]
        public string Solucao { get; set; }
        public bool? AtendimentoRemoto { get; set; }

        [ForeignKey(nameof(CodAutorizada))]
        [InverseProperty(nameof(Autorizadum.FecharOspos))]
        public virtual Autorizadum CodAutorizadaNavigation { get; set; }
        [ForeignKey(nameof(CodDefeitoPos))]
        [InverseProperty(nameof(DefeitoPo.FecharOspos))]
        public virtual DefeitoPo CodDefeitoPosNavigation { get; set; }
        [ForeignKey(nameof(CodMotivoCancelamento))]
        [InverseProperty("FecharOspos")]
        public virtual MotivoCancelamento CodMotivoCancelamentoNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.FecharOspos))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodStatusServico))]
        [InverseProperty(nameof(StatusServico.FecharOspos))]
        public virtual StatusServico CodStatusServicoNavigation { get; set; }
        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.FecharOspos))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
        [ForeignKey(nameof(CodTipoComunicacao))]
        [InverseProperty(nameof(TipoComunicacao.FecharOspos))]
        public virtual TipoComunicacao CodTipoComunicacaoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioEnvio))]
        [InverseProperty(nameof(Usuario.FecharOspos))]
        public virtual Usuario CodUsuarioEnvioNavigation { get; set; }
        [InverseProperty(nameof(FecharOspositen.CodFecharOsposNavigation))]
        public virtual ICollection<FecharOspositen> FecharOspositens { get; set; }
    }
}
