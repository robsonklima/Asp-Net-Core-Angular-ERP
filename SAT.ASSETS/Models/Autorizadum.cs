using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Autorizadum
    {
        public Autorizadum()
        {
            AutorizadaEmails = new HashSet<AutorizadaEmail>();
            AutorizadaRepasses = new HashSet<AutorizadaRepasse>();
            AutorizadaUfcidadePos = new HashSet<AutorizadaUfcidadePo>();
            FecharOspos = new HashSet<FecharOspo>();
            Usuarios = new HashSet<Usuario>();
            ValoresAutorizadaPos = new HashSet<ValoresAutorizadaPo>();
        }

        [Key]
        public int CodAutorizada { get; set; }
        public int CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(20)]
        public string InscricaoEstadual { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(20)]
        public string Bairro { get; set; }
        public int? CodCidade { get; set; }
        [StringLength(300)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Fone { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        public byte IndFilialPerto { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int CodTipoRota { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [Column("AtendePOS")]
        public bool? AtendePos { get; set; }

        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.Autorizada))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.Autorizada))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.AutorizadumCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.AutorizadumCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(AutorizadaEmail.CodAutorizadaNavigation))]
        public virtual ICollection<AutorizadaEmail> AutorizadaEmails { get; set; }
        [InverseProperty(nameof(AutorizadaRepasse.CodAutorizadaNavigation))]
        public virtual ICollection<AutorizadaRepasse> AutorizadaRepasses { get; set; }
        [InverseProperty(nameof(AutorizadaUfcidadePo.CodAutorizadaNavigation))]
        public virtual ICollection<AutorizadaUfcidadePo> AutorizadaUfcidadePos { get; set; }
        [InverseProperty(nameof(FecharOspo.CodAutorizadaNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
        [InverseProperty(nameof(Usuario.CodAutorizadaNavigation))]
        public virtual ICollection<Usuario> Usuarios { get; set; }
        [InverseProperty(nameof(ValoresAutorizadaPo.CodAutorizadaNavigation))]
        public virtual ICollection<ValoresAutorizadaPo> ValoresAutorizadaPos { get; set; }
    }
}
