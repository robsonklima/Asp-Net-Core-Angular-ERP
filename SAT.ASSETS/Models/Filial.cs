using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Filial")]
    [Index(nameof(NomeFilial), Name = "IX_Filial", IsUnique = true)]
    public partial class Filial
    {
        public Filial()
        {
            Autorizada = new HashSet<Autorizadum>();
            AutorizadaRepasses = new HashSet<AutorizadaRepasse>();
            AutorizadaUfcidadePos = new HashSet<AutorizadaUfcidadePo>();
            Cidades = new HashSet<Cidade>();
            DespesaTentativaKms = new HashSet<DespesaTentativaKm>();
            Despesas = new HashSet<Despesa>();
            Orcamento1s = new HashSet<Orcamento1>();
            OrcamentosIsses = new HashSet<OrcamentosIss>();
            TecnicoEquipamentos = new HashSet<TecnicoEquipamento>();
            UsuarioCodFilialNavigations = new HashSet<Usuario>();
            UsuarioCodFilialPontoNavigations = new HashSet<Usuario>();
        }

        [Key]
        public int CodFilial { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(20)]
        public string InscricaoEstadual { get; set; }
        [Column("CEP")]
        [StringLength(20)]
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
        [Column("ICMS", TypeName = "decimal(10, 2)")]
        public decimal? Icms { get; set; }
        [StringLength(4)]
        public string NumColetorPonto { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column("codFilialLogix")]
        public int? CodFilialLogix { get; set; }
        [Column("codUsuarioSupervisor")]
        [StringLength(20)]
        public string CodUsuarioSupervisor { get; set; }

        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.FilialCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.FilialCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(Autorizadum.CodFilialNavigation))]
        public virtual ICollection<Autorizadum> Autorizada { get; set; }
        [InverseProperty(nameof(AutorizadaRepasse.CodFilialNavigation))]
        public virtual ICollection<AutorizadaRepasse> AutorizadaRepasses { get; set; }
        [InverseProperty(nameof(AutorizadaUfcidadePo.CodFilialNavigation))]
        public virtual ICollection<AutorizadaUfcidadePo> AutorizadaUfcidadePos { get; set; }
        [InverseProperty(nameof(Cidade.CodFilialNavigation))]
        public virtual ICollection<Cidade> Cidades { get; set; }
        [InverseProperty(nameof(DespesaTentativaKm.CodFilialNavigation))]
        public virtual ICollection<DespesaTentativaKm> DespesaTentativaKms { get; set; }
        [InverseProperty(nameof(Despesa.CodFilialNavigation))]
        public virtual ICollection<Despesa> Despesas { get; set; }
        [InverseProperty(nameof(Orcamento1.CodFilialNavigation))]
        public virtual ICollection<Orcamento1> Orcamento1s { get; set; }
        [InverseProperty(nameof(OrcamentosIss.CodFilialNavigation))]
        public virtual ICollection<OrcamentosIss> OrcamentosIsses { get; set; }
        [InverseProperty(nameof(TecnicoEquipamento.CodFilialNavigation))]
        public virtual ICollection<TecnicoEquipamento> TecnicoEquipamentos { get; set; }
        [InverseProperty(nameof(Usuario.CodFilialNavigation))]
        public virtual ICollection<Usuario> UsuarioCodFilialNavigations { get; set; }
        [InverseProperty(nameof(Usuario.CodFilialPontoNavigation))]
        public virtual ICollection<Usuario> UsuarioCodFilialPontoNavigations { get; set; }
    }
}
