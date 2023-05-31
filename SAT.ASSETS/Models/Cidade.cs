using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Cidade")]
    public partial class Cidade
    {
        public Cidade()
        {
            Autorizada = new HashSet<Autorizadum>();
            AutorizadaUfcidadePos = new HashSet<AutorizadaUfcidadePo>();
            CalendarioDiaFeriados = new HashSet<CalendarioDiaFeriado>();
            CidadeDeParaClientes = new HashSet<CidadeDeParaCliente>();
            CidadePosdeParas = new HashSet<CidadePosdePara>();
            ClienteBancada = new HashSet<ClienteBancadum>();
            Clientes = new HashSet<Cliente>();
            ContaCorrentes = new HashSet<ContaCorrente>();
            DespesaItemCodCidadeDestinoNavigations = new HashSet<DespesaItem>();
            DespesaItemCodCidadeOrigemNavigations = new HashSet<DespesaItem>();
            DespesaTentativaKmCodCidadeDestinoNavigations = new HashSet<DespesaTentativaKm>();
            DespesaTentativaKmCodCidadeOrigemNavigations = new HashSet<DespesaTentativaKm>();
            FeriadosPos = new HashSet<FeriadosPo>();
            LocalEnvioNffaturamentoCodCidadeEnvioNfNavigations = new HashSet<LocalEnvioNffaturamento>();
            LocalEnvioNffaturamentoCodCidadeFaturamentoNavigations = new HashSet<LocalEnvioNffaturamento>();
            SlasicrediPos = new HashSet<SlasicrediPo>();
            Transportadoras = new HashSet<Transportadora>();
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int CodCidade { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int CodFilial { get; set; }
        [Column("CodSLAParametroAdicional")]
        public byte? CodSlaparametroAdicional { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [StringLength(53)]
        public string Latitude { get; set; }
        [StringLength(53)]
        public string Longitude { get; set; }
        public byte IndAtivo { get; set; }
        public int IndConsulta { get; set; }
        public byte? IndCapital { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column("latitude_metros")]
        public double? LatitudeMetros { get; set; }
        [Column("longitude_metros")]
        public double? LongitudeMetros { get; set; }
        public int? Regiao { get; set; }
        [Column("Horas_RAcesso")]
        public int? HorasRacesso { get; set; }
        [Column("CodRegiaoPOS")]
        public int? CodRegiaoPos { get; set; }

        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.Cidades))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodRegiaoPos))]
        [InverseProperty(nameof(RegiaoPo.Cidades))]
        public virtual RegiaoPo CodRegiaoPosNavigation { get; set; }
        [ForeignKey(nameof(CodUf))]
        [InverseProperty(nameof(Uf.Cidades))]
        public virtual Uf CodUfNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.CidadeCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.CidadeCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty("CodCidadeNavigation")]
        public virtual CidadeFonetica CidadeFonetica { get; set; }
        [InverseProperty(nameof(Autorizadum.CodCidadeNavigation))]
        public virtual ICollection<Autorizadum> Autorizada { get; set; }
        [InverseProperty(nameof(AutorizadaUfcidadePo.CodCidadeNavigation))]
        public virtual ICollection<AutorizadaUfcidadePo> AutorizadaUfcidadePos { get; set; }
        [InverseProperty(nameof(CalendarioDiaFeriado.CodCidadeNavigation))]
        public virtual ICollection<CalendarioDiaFeriado> CalendarioDiaFeriados { get; set; }
        [InverseProperty(nameof(CidadeDeParaCliente.CodCidadeNavigation))]
        public virtual ICollection<CidadeDeParaCliente> CidadeDeParaClientes { get; set; }
        [InverseProperty(nameof(CidadePosdePara.CodCidadeNavigation))]
        public virtual ICollection<CidadePosdePara> CidadePosdeParas { get; set; }
        [InverseProperty(nameof(ClienteBancadum.CodCidadeNavigation))]
        public virtual ICollection<ClienteBancadum> ClienteBancada { get; set; }
        [InverseProperty(nameof(Cliente.CodCidadeNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [InverseProperty(nameof(ContaCorrente.CodCidadeNavigation))]
        public virtual ICollection<ContaCorrente> ContaCorrentes { get; set; }
        [InverseProperty(nameof(DespesaItem.CodCidadeDestinoNavigation))]
        public virtual ICollection<DespesaItem> DespesaItemCodCidadeDestinoNavigations { get; set; }
        [InverseProperty(nameof(DespesaItem.CodCidadeOrigemNavigation))]
        public virtual ICollection<DespesaItem> DespesaItemCodCidadeOrigemNavigations { get; set; }
        [InverseProperty(nameof(DespesaTentativaKm.CodCidadeDestinoNavigation))]
        public virtual ICollection<DespesaTentativaKm> DespesaTentativaKmCodCidadeDestinoNavigations { get; set; }
        [InverseProperty(nameof(DespesaTentativaKm.CodCidadeOrigemNavigation))]
        public virtual ICollection<DespesaTentativaKm> DespesaTentativaKmCodCidadeOrigemNavigations { get; set; }
        [InverseProperty(nameof(FeriadosPo.CodCidadeNavigation))]
        public virtual ICollection<FeriadosPo> FeriadosPos { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamento.CodCidadeEnvioNfNavigation))]
        public virtual ICollection<LocalEnvioNffaturamento> LocalEnvioNffaturamentoCodCidadeEnvioNfNavigations { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamento.CodCidadeFaturamentoNavigation))]
        public virtual ICollection<LocalEnvioNffaturamento> LocalEnvioNffaturamentoCodCidadeFaturamentoNavigations { get; set; }
        [InverseProperty(nameof(SlasicrediPo.CodCidadeNavigation))]
        public virtual ICollection<SlasicrediPo> SlasicrediPos { get; set; }
        [InverseProperty(nameof(Transportadora.CodCidadeNavigation))]
        public virtual ICollection<Transportadora> Transportadoras { get; set; }
        [InverseProperty(nameof(Usuario.CodCidadeNavigation))]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
