using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OS")]
    public partial class O
    {
        public O()
        {
            AtendimentoTelefonicoPos = new HashSet<AtendimentoTelefonicoPo>();
            BaixaNaoRealizada = new HashSet<BaixaNaoRealizadum>();
            Chamados = new HashSet<Chamado>();
            ControleEnvioEmailCds = new HashSet<ControleEnvioEmailCd>();
            ControleEnvioEmailCdserros = new HashSet<ControleEnvioEmailCdserro>();
            ControleEnvioEmailCdsreenvios = new HashSet<ControleEnvioEmailCdsreenvio>();
            ControleEnvioEmailVipErros = new HashSet<ControleEnvioEmailVipErro>();
            ControleEnvioEmailVips = new HashSet<ControleEnvioEmailVip>();
            EquipamentoPoshists = new HashSet<EquipamentoPoshist>();
            FaturamentoSicrediMultaSaldoItemLancados = new HashSet<FaturamentoSicrediMultaSaldoItemLancado>();
            FaturamentoSicrediMultaSaldoItemMulta = new HashSet<FaturamentoSicrediMultaSaldoItemMultum>();
            FecharOspos = new HashSet<FecharOspo>();
            IntegracaoCaixas = new HashSet<IntegracaoCaixa>();
            OscopiaPoCodOsdestinoNavigations = new HashSet<OscopiaPo>();
            OscopiaPoCodOsorigemNavigations = new HashSet<OscopiaPo>();
            OshistPos = new HashSet<OshistPo>();
            Osposimagens = new HashSet<Osposimagen>();
            OspossicrediCobrancaAtendimentos = new HashSet<OspossicrediCobrancaAtendimento>();
            Osreaberturas = new HashSet<Osreabertura>();
            OsressarcirCobrancaSicrediPos = new HashSet<OsressarcirCobrancaSicrediPo>();
            OsretiradaPoCodOsNavigations = new HashSet<OsretiradaPo>();
            OsretiradaPoCodOsinstalacaoNavigations = new HashSet<OsretiradaPo>();
        }

        [Key]
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodCliente { get; set; }
        public int CodPosto { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodEquip { get; set; }
        public int CodTipoIntervencao { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int CodStatusServico { get; set; }
        public int? CodTecnico { get; set; }
        [Column("DescNumSerieNI")]
        [StringLength(20)]
        public string DescNumSerieNi { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolicitacao { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column("NumOSQuarteirizada")]
        [StringLength(20)]
        public string NumOsquarteirizada { get; set; }
        [StringLength(255)]
        public string NomeSolicitante { get; set; }
        [StringLength(20)]
        public string TelefoneSolicitante { get; set; }
        [StringLength(50)]
        public string NomeContato { get; set; }
        public string ObservacaoCliente { get; set; }
        [StringLength(1000)]
        public string DescMotivoMarcaEspecial { get; set; }
        public byte? IndMarcaEspecial { get; set; }
        [StringLength(20)]
        public string CodUsuarioMarcaEspecial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataMarcaEspecial { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraTransf { get; set; }
        [StringLength(20)]
        public string CodUsuarioFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
        [StringLength(1000)]
        public string MotivoCancelamento { get; set; }
        [StringLength(20)]
        public string CodUsuarioCancelamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCancelamento { get; set; }
        public byte IndRevisaoReincidencia { get; set; }
        public byte? IndCienciaAtendente { get; set; }
        public byte? IndCienciaTerceirizada { get; set; }
        public byte? IndServico { get; set; }
        [StringLength(100)]
        public string ServicoEmail { get; set; }
        [Column("NumAgenciaNI")]
        [StringLength(10)]
        public string NumAgenciaNi { get; set; }
        [StringLength(5)]
        public string NumRemessa { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodTipoMarcacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaReparo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaSolucao { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(20)]
        public string Bairro { get; set; }
        [StringLength(30)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(2)]
        public string SiglaUf { get; set; }
        [StringLength(20)]
        public string Pais { get; set; }
        [StringLength(20)]
        public string Cep { get; set; }
        public int? NumPatrimonio { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSolicAtendimento { get; set; }
        public byte? IndCancelado { get; set; }
        public byte? IndReincidencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column("IndAtrasoSLA")]
        public byte? IndAtrasoSla { get; set; }
        public float? TempoEfetInicio { get; set; }
        public float? TempoEfetReparo { get; set; }
        public float? TempoEfetSolucao { get; set; }
        public byte? IndAcertoParque { get; set; }
        public byte? IndEndossado { get; set; }
        [StringLength(20)]
        public string CodUsuarioAcertoParque { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAcertoParque { get; set; }
        [Column("IndRevOK")]
        public byte? IndRevOk { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public byte? IndOrcamentoEnd { get; set; }
        public byte? IndEnderecoRevisado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnderecoVerificado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraIntegracaoRevisao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraIntegracaoRevisaoAgendamento { get; set; }
        public byte? IndAgendamentoReenviado { get; set; }
        [Column("DataHoraOSMobileRecebida", TypeName = "datetime")]
        public DateTime? DataHoraOsmobileRecebida { get; set; }
        [Column("DataHoraOSMobileLida", TypeName = "datetime")]
        public DateTime? DataHoraOsmobileLida { get; set; }
        [StringLength(30)]
        public string NumAgenciaBanco { get; set; }
        [StringLength(40)]
        public string NumContaEstabelecimentoCliente { get; set; }
        [Column("CNPJEstabelecimentoCliente")]
        [StringLength(30)]
        public string CnpjestabelecimentoCliente { get; set; }
        [StringLength(50)]
        public string RazaoSocialEstabelecimentoCliente { get; set; }
        [StringLength(50)]
        public string RedeEquipamento { get; set; }
        [StringLength(35)]
        public string NumTerminal { get; set; }
        public byte? IndServicoVerificado { get; set; }
        public int? IndIntegracao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnvioAgendamentoSemat { get; set; }
        public byte? IndAgendamentoUnico { get; set; }
        public byte? IndNotificacaoOrcamentoEnviado { get; set; }
        public int? CodMotivoCancelamentoBanrisul { get; set; }
        [StringLength(30)]
        public string NomeArquivoIntegracaoBanrisul { get; set; }
        public byte? IndReaberturaIntegracaoBanrisul { get; set; }
        [Column("IndOSIntervencaoEquipamento")]
        public byte? IndOsintervencaoEquipamento { get; set; }
        public byte? IndLiberacaoFechaduraCofre { get; set; }
        public byte? IndExclusaoBanrisul { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusaoBanrisul { get; set; }
        public int? IndBloqueioReincidencia { get; set; }
        public int? NumReincidencia { get; set; }
        public byte? IndVandalismo { get; set; }
        [Column("indStatusEnvioReincidencia")]
        public int IndStatusEnvioReincidencia { get; set; }
        public byte? IndFechamentoBanrisul { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFechamentoBanrisul { get; set; }
        [StringLength(50)]
        public string ObsFechamentoBanrisul { get; set; }
        [StringLength(15)]
        public string NumEstabelecimentoCliente { get; set; }
        public int? CodSeveridade { get; set; }
        public int? CodContrato { get; set; }
        [Column("SugestaoOS")]
        [StringLength(250)]
        public string SugestaoOs { get; set; }
        public int? CodMotivoCancelamento { get; set; }
        public int? CodOperadoraTelefonia { get; set; }
        [Column("CodDefeitoPOS")]
        public int? CodDefeitoPos { get; set; }
        [Column("CodUsuarioOSMobileLida")]
        [StringLength(20)]
        public string CodUsuarioOsmobileLida { get; set; }
        [Column("CodUsuarioOSMobileRecebida")]
        [StringLength(20)]
        public string CodUsuarioOsmobileRecebida { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraIntegracaoRevisaoV2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraIntegracaoRevisaoAgendamentoV2 { get; set; }

        [ForeignKey(nameof(CodDefeitoPos))]
        [InverseProperty(nameof(DefeitoPo.Os))]
        public virtual DefeitoPo CodDefeitoPosNavigation { get; set; }
        [ForeignKey(nameof(CodMotivoCancelamento))]
        [InverseProperty("Os")]
        public virtual MotivoCancelamento CodMotivoCancelamentoNavigation { get; set; }
        [ForeignKey(nameof(CodOperadoraTelefonia))]
        [InverseProperty(nameof(OperadoraTelefonium.Os))]
        public virtual OperadoraTelefonium CodOperadoraTelefoniaNavigation { get; set; }
        [InverseProperty("CodOsNavigation")]
        public virtual ChamadoO ChamadoO { get; set; }
        [InverseProperty("CodOsNavigation")]
        public virtual OscanceladaPossicredi OscanceladaPossicredi { get; set; }
        [InverseProperty(nameof(AtendimentoTelefonicoPo.CodOsNavigation))]
        public virtual ICollection<AtendimentoTelefonicoPo> AtendimentoTelefonicoPos { get; set; }
        [InverseProperty(nameof(BaixaNaoRealizadum.CodOsNavigation))]
        public virtual ICollection<BaixaNaoRealizadum> BaixaNaoRealizada { get; set; }
        [InverseProperty(nameof(Chamado.CodOsNavigation))]
        public virtual ICollection<Chamado> Chamados { get; set; }
        [InverseProperty(nameof(ControleEnvioEmailCd.CodOsNavigation))]
        public virtual ICollection<ControleEnvioEmailCd> ControleEnvioEmailCds { get; set; }
        [InverseProperty(nameof(ControleEnvioEmailCdserro.CodOsNavigation))]
        public virtual ICollection<ControleEnvioEmailCdserro> ControleEnvioEmailCdserros { get; set; }
        [InverseProperty(nameof(ControleEnvioEmailCdsreenvio.CodOsNavigation))]
        public virtual ICollection<ControleEnvioEmailCdsreenvio> ControleEnvioEmailCdsreenvios { get; set; }
        [InverseProperty(nameof(ControleEnvioEmailVipErro.CodOsNavigation))]
        public virtual ICollection<ControleEnvioEmailVipErro> ControleEnvioEmailVipErros { get; set; }
        [InverseProperty(nameof(ControleEnvioEmailVip.CodOsNavigation))]
        public virtual ICollection<ControleEnvioEmailVip> ControleEnvioEmailVips { get; set; }
        [InverseProperty(nameof(EquipamentoPoshist.CodOsNavigation))]
        public virtual ICollection<EquipamentoPoshist> EquipamentoPoshists { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldoItemLancado.CodOsNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldoItemLancado> FaturamentoSicrediMultaSaldoItemLancados { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldoItemMultum.CodOsNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldoItemMultum> FaturamentoSicrediMultaSaldoItemMulta { get; set; }
        [InverseProperty(nameof(FecharOspo.CodOsNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
        [InverseProperty(nameof(IntegracaoCaixa.CodOsNavigation))]
        public virtual ICollection<IntegracaoCaixa> IntegracaoCaixas { get; set; }
        [InverseProperty(nameof(OscopiaPo.CodOsdestinoNavigation))]
        public virtual ICollection<OscopiaPo> OscopiaPoCodOsdestinoNavigations { get; set; }
        [InverseProperty(nameof(OscopiaPo.CodOsorigemNavigation))]
        public virtual ICollection<OscopiaPo> OscopiaPoCodOsorigemNavigations { get; set; }
        [InverseProperty(nameof(OshistPo.CodOsNavigation))]
        public virtual ICollection<OshistPo> OshistPos { get; set; }
        [InverseProperty(nameof(Osposimagen.CodOsNavigation))]
        public virtual ICollection<Osposimagen> Osposimagens { get; set; }
        [InverseProperty(nameof(OspossicrediCobrancaAtendimento.CodOsNavigation))]
        public virtual ICollection<OspossicrediCobrancaAtendimento> OspossicrediCobrancaAtendimentos { get; set; }
        [InverseProperty(nameof(Osreabertura.CodOsNavigation))]
        public virtual ICollection<Osreabertura> Osreaberturas { get; set; }
        [InverseProperty(nameof(OsressarcirCobrancaSicrediPo.CodOsNavigation))]
        public virtual ICollection<OsressarcirCobrancaSicrediPo> OsressarcirCobrancaSicrediPos { get; set; }
        [InverseProperty(nameof(OsretiradaPo.CodOsNavigation))]
        public virtual ICollection<OsretiradaPo> OsretiradaPoCodOsNavigations { get; set; }
        [InverseProperty(nameof(OsretiradaPo.CodOsinstalacaoNavigation))]
        public virtual ICollection<OsretiradaPo> OsretiradaPoCodOsinstalacaoNavigations { get; set; }
    }
}
