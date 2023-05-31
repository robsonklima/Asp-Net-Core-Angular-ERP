using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            AcaoPoCodUsuarioCadastroNavigations = new HashSet<AcaoPo>();
            AcaoPoCodUsuarioManutencaoNavigations = new HashSet<AcaoPo>();
            AguardandoClientePos = new HashSet<AguardandoClientePo>();
            AtendimentoTelefonicoPos = new HashSet<AtendimentoTelefonicoPo>();
            AutorizadaEmails = new HashSet<AutorizadaEmail>();
            AutorizadumCodUsuarioCadNavigations = new HashSet<Autorizadum>();
            AutorizadumCodUsuarioManutNavigations = new HashSet<Autorizadum>();
            BaixaNaoRealizada = new HashSet<BaixaNaoRealizadum>();
            BaseEquipamentoBanrisuls = new HashSet<BaseEquipamentoBanrisul>();
            ChamadoCodUsuarioAtendimentoNavigations = new HashSet<Chamado>();
            ChamadoCodUsuarioCadastroNavigations = new HashSet<Chamado>();
            ChamadoCodUsuarioOsNavigations = new HashSet<Chamado>();
            ChamadoDadosAdicionais = new HashSet<ChamadoDadosAdicionai>();
            ChamadoHists = new HashSet<ChamadoHist>();
            CidadeCodUsuarioCadNavigations = new HashSet<Cidade>();
            CidadeCodUsuarioManutNavigations = new HashSet<Cidade>();
            ClienteUsuarios = new HashSet<ClienteUsuario>();
            ComponentePos = new HashSet<ComponentePo>();
            ConjuntoCodUsuarioCadNavigations = new HashSet<Conjunto>();
            ConjuntoCodUsuarioManutNavigations = new HashSet<Conjunto>();
            ConjuntoConjuntoCodUsuarioCadNavigations = new HashSet<ConjuntoConjunto>();
            ConjuntoConjuntoCodUsuarioManutNavigations = new HashSet<ConjuntoConjunto>();
            ConjuntoPecaCodUsuarioCadNavigations = new HashSet<ConjuntoPeca>();
            ConjuntoPecaCodUsuarioManutNavigations = new HashSet<ConjuntoPeca>();
            ContratoEquipamentoCodUsuarioCadNavigations = new HashSet<ContratoEquipamento>();
            ContratoEquipamentoCodUsuarioManutNavigations = new HashSet<ContratoEquipamento>();
            ControleEnvioEmailCds = new HashSet<ControleEnvioEmailCd>();
            ControleEnvioEmailCdsreenvios = new HashSet<ControleEnvioEmailCdsreenvio>();
            DefeitoPos = new HashSet<DefeitoPo>();
            DespesaAdiantamentoPeriodos = new HashSet<DespesaAdiantamentoPeriodo>();
            DespesaProtocoloCodUsuarioCadNavigations = new HashSet<DespesaProtocolo>();
            DespesaProtocoloCodUsuarioManutNavigations = new HashSet<DespesaProtocolo>();
            EquipamentoPoshists = new HashSet<EquipamentoPoshist>();
            FaturamentoPosbanrisuls = new HashSet<FaturamentoPosbanrisul>();
            FaturamentoSicrediMultaSaldoItemLancados = new HashSet<FaturamentoSicrediMultaSaldoItemLancado>();
            FaturamentoSicrediMultaSaldoItemMulta = new HashSet<FaturamentoSicrediMultaSaldoItemMultum>();
            FaturamentoSicrediMultaSaldos = new HashSet<FaturamentoSicrediMultaSaldo>();
            FaturamentoSicredis = new HashSet<FaturamentoSicredi>();
            FechamentoOspos = new HashSet<FechamentoOspo>();
            FecharOspos = new HashSet<FecharOspo>();
            FeriadosPos = new HashSet<FeriadosPo>();
            FilialCodUsuarioCadNavigations = new HashSet<Filial>();
            FilialCodUsuarioManutNavigations = new HashSet<Filial>();
            IncidenteNaoProcedentes = new HashSet<IncidenteNaoProcedente>();
            InstalAnexoCodUsuarioCadNavigations = new HashSet<InstalAnexo>();
            InstalAnexoCodUsuarioManutNavigations = new HashSet<InstalAnexo>();
            InstalObCodUsuarioCadNavigations = new HashSet<InstalOb>();
            InstalObCodUsuarioManutNavigations = new HashSet<InstalOb>();
            InstalOs = new HashSet<InstalO>();
            InstalPagtoCodUsuarioCadNavigations = new HashSet<InstalPagto>();
            InstalPagtoCodUsuarioManutNavigations = new HashSet<InstalPagto>();
            InstalPleitoCodUsuarioCadNavigations = new HashSet<InstalPleito>();
            InstalPleitoCodUsuarioManutNavigations = new HashSet<InstalPleito>();
            LaboratorioPos = new HashSet<LaboratorioPo>();
            MensagemUsuarios = new HashSet<MensagemUsuario>();
            MotivoReaberturaOs = new HashSet<MotivoReaberturaO>();
            NotaFaturamentoSicredis = new HashSet<NotaFaturamentoSicredi>();
            NumeroSerieControleEquipamentos = new HashSet<NumeroSerieControleEquipamento>();
            NumeroSerieControleTitulos = new HashSet<NumeroSerieControleTitulo>();
            OscanceladaPossicredis = new HashSet<OscanceladaPossicredi>();
            OscopiaPos = new HashSet<OscopiaPo>();
            OshistPos = new HashSet<OshistPo>();
            Osposimagens = new HashSet<Osposimagen>();
            Osreaberturas = new HashSet<Osreabertura>();
            OsressarcirCobrancaSicrediPos = new HashSet<OsressarcirCobrancaSicrediPo>();
            PagamentosPos = new HashSet<PagamentosPo>();
            PaiCodUsuarioCadNavigations = new HashSet<Pai>();
            PaiCodUsuarioManutNavigations = new HashSet<Pai>();
            PatrimonioPos = new HashSet<PatrimonioPo>();
            PedidoInvoices = new HashSet<PedidoInvoice>();
            PontoColaboradors = new HashSet<PontoColaborador>();
            PontoMovels = new HashSet<PontoMovel>();
            PontoSobreAvisos = new HashSet<PontoSobreAviso>();
            PontoUsuarios = new HashSet<PontoUsuario>();
            Pontos = new HashSet<Ponto>();
            PosvendaBanrisulCodUsuarioCadastroNavigations = new HashSet<PosvendaBanrisul>();
            PosvendaBanrisulCodUsuarioDesativacaoNavigations = new HashSet<PosvendaBanrisul>();
            ProducaoPos = new HashSet<ProducaoPo>();
            RelatorioApresentacaoCodUsuarioCadNavigations = new HashSet<RelatorioApresentacao>();
            RelatorioApresentacaoCodUsuarioManutNavigations = new HashSet<RelatorioApresentacao>();
            RelatorioCodUsuarioCadNavigations = new HashSet<Relatorio>();
            RelatorioCodUsuarioManutNavigations = new HashSet<Relatorio>();
            RelatorioPerfils = new HashSet<RelatorioPerfil>();
            RelatorioUsuarioCodUsuarioCadNavigations = new HashSet<RelatorioUsuario>();
            RelatorioUsuarioCodUsuarioNavigations = new HashSet<RelatorioUsuario>();
            RelatorioVisaoCodUsuarioCadNavigations = new HashSet<RelatorioVisao>();
            RelatorioVisaoCodUsuarioManutNavigations = new HashSet<RelatorioVisao>();
            RelatorioVisaoUsuarioCodUsuarioCadNavigations = new HashSet<RelatorioVisaoUsuario>();
            RelatorioVisaoUsuarioCodUsuarioNavigations = new HashSet<RelatorioVisaoUsuario>();
            SessaoUsuarios = new HashSet<SessaoUsuario>();
            TarefaNotificacaos = new HashSet<TarefaNotificacao>();
            Tarefas = new HashSet<Tarefa>();
            TecnicoEquipamentos = new HashSet<TecnicoEquipamento>();
            TipoUsuarios = new HashSet<TipoUsuario>();
            UfCodUsuarioCadNavigations = new HashSet<Uf>();
            UfCodUsuarioManutNavigations = new HashSet<Uf>();
            UsuarioDashboards = new HashSet<UsuarioDashboard>();
            UsuarioMenus = new HashSet<UsuarioMenu>();
            UsuarioNovaSenhas = new HashSet<UsuarioNovaSenha>();
            UsuarioTipoUsuarioCodUsuarioCadastroNavigations = new HashSet<UsuarioTipoUsuario>();
            ValoresAutorizadaPos = new HashSet<ValoresAutorizadaPo>();
            VioladosPosbanrisuls = new HashSet<VioladosPosbanrisul>();
        }

        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodCliente { get; set; }
        public int? CodCargo { get; set; }
        public int? CodDepartamento { get; set; }
        public int? CodTurno { get; set; }
        public int? CodCidade { get; set; }
        public int? CodFilialPonto { get; set; }
        public int CodFusoHorario { get; set; }
        public int CodLingua { get; set; }
        public int? CodPerfil { get; set; }
        public int? CodSmartCard { get; set; }
        [StringLength(150)]
        public string CodContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAdmissao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        public byte? CodPeca { get; set; }
        [Column("CPF")]
        [StringLength(20)]
        public string Cpf { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(30)]
        public string Bairro { get; set; }
        [StringLength(40)]
        public string Fone { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(20)]
        public string Ramal { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(6)]
        public string NumCracha { get; set; }
        [StringLength(100)]
        public string CodRelatorioNaoMostrado { get; set; }
        [StringLength(2000)]
        public string InstalPerfilPagina { get; set; }
        [Required]
        [StringLength(255)]
        public string Senha { get; set; }
        public byte IndAtivo { get; set; }
        public byte? IndAssinaInvoice { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public byte? IndPonto { get; set; }
        public byte? IndBloqueio { get; set; }
        [StringLength(50)]
        public string Numero { get; set; }
        [StringLength(50)]
        public string Complemento { get; set; }
        public int? CodTransportadora { get; set; }
        [Column("IndPermiteRegistrarEquipPOS")]
        public bool? IndPermiteRegistrarEquipPos { get; set; }

        [ForeignKey(nameof(CodAutorizada))]
        [InverseProperty(nameof(Autorizadum.Usuarios))]
        public virtual Autorizadum CodAutorizadaNavigation { get; set; }
        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.Usuarios))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.UsuarioCodFilialNavigations))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodFilialPonto))]
        [InverseProperty(nameof(Filial.UsuarioCodFilialPontoNavigations))]
        public virtual Filial CodFilialPontoNavigation { get; set; }
        [InverseProperty("CodUsuarioNavigation")]
        public virtual UsuarioPermissaoEspecial UsuarioPermissaoEspecial { get; set; }
        [InverseProperty(nameof(UsuarioTipoUsuario.CodUsuarioNavigation))]
        public virtual UsuarioTipoUsuario UsuarioTipoUsuarioCodUsuarioNavigation { get; set; }
        [InverseProperty(nameof(AcaoPo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<AcaoPo> AcaoPoCodUsuarioCadastroNavigations { get; set; }
        [InverseProperty(nameof(AcaoPo.CodUsuarioManutencaoNavigation))]
        public virtual ICollection<AcaoPo> AcaoPoCodUsuarioManutencaoNavigations { get; set; }
        [InverseProperty(nameof(AguardandoClientePo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<AguardandoClientePo> AguardandoClientePos { get; set; }
        [InverseProperty(nameof(AtendimentoTelefonicoPo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<AtendimentoTelefonicoPo> AtendimentoTelefonicoPos { get; set; }
        [InverseProperty(nameof(AutorizadaEmail.CodUsuarioCadNavigation))]
        public virtual ICollection<AutorizadaEmail> AutorizadaEmails { get; set; }
        [InverseProperty(nameof(Autorizadum.CodUsuarioCadNavigation))]
        public virtual ICollection<Autorizadum> AutorizadumCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(Autorizadum.CodUsuarioManutNavigation))]
        public virtual ICollection<Autorizadum> AutorizadumCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(BaixaNaoRealizadum.UsuarioCadastroNavigation))]
        public virtual ICollection<BaixaNaoRealizadum> BaixaNaoRealizada { get; set; }
        [InverseProperty(nameof(BaseEquipamentoBanrisul.CodUsuarioCadastroNavigation))]
        public virtual ICollection<BaseEquipamentoBanrisul> BaseEquipamentoBanrisuls { get; set; }
        [InverseProperty(nameof(Chamado.CodUsuarioAtendimentoNavigation))]
        public virtual ICollection<Chamado> ChamadoCodUsuarioAtendimentoNavigations { get; set; }
        [InverseProperty(nameof(Chamado.CodUsuarioCadastroNavigation))]
        public virtual ICollection<Chamado> ChamadoCodUsuarioCadastroNavigations { get; set; }
        [InverseProperty(nameof(Chamado.CodUsuarioOsNavigation))]
        public virtual ICollection<Chamado> ChamadoCodUsuarioOsNavigations { get; set; }
        [InverseProperty(nameof(ChamadoDadosAdicionai.CodUsuarioCadastroNavigation))]
        public virtual ICollection<ChamadoDadosAdicionai> ChamadoDadosAdicionais { get; set; }
        [InverseProperty(nameof(ChamadoHist.CodUsuarioNavigation))]
        public virtual ICollection<ChamadoHist> ChamadoHists { get; set; }
        [InverseProperty(nameof(Cidade.CodUsuarioCadNavigation))]
        public virtual ICollection<Cidade> CidadeCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(Cidade.CodUsuarioManutNavigation))]
        public virtual ICollection<Cidade> CidadeCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(ClienteUsuario.CodUsuarioNavigation))]
        public virtual ICollection<ClienteUsuario> ClienteUsuarios { get; set; }
        [InverseProperty(nameof(ComponentePo.CodUsuarioManutencaoNavigation))]
        public virtual ICollection<ComponentePo> ComponentePos { get; set; }
        [InverseProperty(nameof(Conjunto.CodUsuarioCadNavigation))]
        public virtual ICollection<Conjunto> ConjuntoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(Conjunto.CodUsuarioManutNavigation))]
        public virtual ICollection<Conjunto> ConjuntoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(ConjuntoConjunto.CodUsuarioCadNavigation))]
        public virtual ICollection<ConjuntoConjunto> ConjuntoConjuntoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(ConjuntoConjunto.CodUsuarioManutNavigation))]
        public virtual ICollection<ConjuntoConjunto> ConjuntoConjuntoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(ConjuntoPeca.CodUsuarioCadNavigation))]
        public virtual ICollection<ConjuntoPeca> ConjuntoPecaCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(ConjuntoPeca.CodUsuarioManutNavigation))]
        public virtual ICollection<ConjuntoPeca> ConjuntoPecaCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(ContratoEquipamento.CodUsuarioCadNavigation))]
        public virtual ICollection<ContratoEquipamento> ContratoEquipamentoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(ContratoEquipamento.CodUsuarioManutNavigation))]
        public virtual ICollection<ContratoEquipamento> ContratoEquipamentoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(ControleEnvioEmailCd.CodUsuarioNavigation))]
        public virtual ICollection<ControleEnvioEmailCd> ControleEnvioEmailCds { get; set; }
        [InverseProperty(nameof(ControleEnvioEmailCdsreenvio.CodUsuarioNavigation))]
        public virtual ICollection<ControleEnvioEmailCdsreenvio> ControleEnvioEmailCdsreenvios { get; set; }
        [InverseProperty(nameof(DefeitoPo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<DefeitoPo> DefeitoPos { get; set; }
        [InverseProperty(nameof(DespesaAdiantamentoPeriodo.CodUsuarioCadNavigation))]
        public virtual ICollection<DespesaAdiantamentoPeriodo> DespesaAdiantamentoPeriodos { get; set; }
        [InverseProperty(nameof(DespesaProtocolo.CodUsuarioCadNavigation))]
        public virtual ICollection<DespesaProtocolo> DespesaProtocoloCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(DespesaProtocolo.CodUsuarioManutNavigation))]
        public virtual ICollection<DespesaProtocolo> DespesaProtocoloCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(EquipamentoPoshist.CodUsuarioNavigation))]
        public virtual ICollection<EquipamentoPoshist> EquipamentoPoshists { get; set; }
        [InverseProperty(nameof(FaturamentoPosbanrisul.CodUsuarioNavigation))]
        public virtual ICollection<FaturamentoPosbanrisul> FaturamentoPosbanrisuls { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldoItemLancado.CodUsuarioNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldoItemLancado> FaturamentoSicrediMultaSaldoItemLancados { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldoItemMultum.CodUsuarioNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldoItemMultum> FaturamentoSicrediMultaSaldoItemMulta { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldo.CodUsuarioNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldo> FaturamentoSicrediMultaSaldos { get; set; }
        [InverseProperty(nameof(FaturamentoSicredi.CodUsuarioNavigation))]
        public virtual ICollection<FaturamentoSicredi> FaturamentoSicredis { get; set; }
        [InverseProperty(nameof(FechamentoOspo.CodUsuarioNavigation))]
        public virtual ICollection<FechamentoOspo> FechamentoOspos { get; set; }
        [InverseProperty(nameof(FecharOspo.CodUsuarioEnvioNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
        [InverseProperty(nameof(FeriadosPo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<FeriadosPo> FeriadosPos { get; set; }
        [InverseProperty(nameof(Filial.CodUsuarioCadNavigation))]
        public virtual ICollection<Filial> FilialCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(Filial.CodUsuarioManutNavigation))]
        public virtual ICollection<Filial> FilialCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(IncidenteNaoProcedente.UsuarioCadastroNavigation))]
        public virtual ICollection<IncidenteNaoProcedente> IncidenteNaoProcedentes { get; set; }
        [InverseProperty(nameof(InstalAnexo.CodUsuarioCadNavigation))]
        public virtual ICollection<InstalAnexo> InstalAnexoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(InstalAnexo.CodUsuarioManutNavigation))]
        public virtual ICollection<InstalAnexo> InstalAnexoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(InstalOb.CodUsuarioCadNavigation))]
        public virtual ICollection<InstalOb> InstalObCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(InstalOb.CodUsuarioManutNavigation))]
        public virtual ICollection<InstalOb> InstalObCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(InstalO.CodUsuarioCadNavigation))]
        public virtual ICollection<InstalO> InstalOs { get; set; }
        [InverseProperty(nameof(InstalPagto.CodUsuarioCadNavigation))]
        public virtual ICollection<InstalPagto> InstalPagtoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(InstalPagto.CodUsuarioManutNavigation))]
        public virtual ICollection<InstalPagto> InstalPagtoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(InstalPleito.CodUsuarioCadNavigation))]
        public virtual ICollection<InstalPleito> InstalPleitoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(InstalPleito.CodUsuarioManutNavigation))]
        public virtual ICollection<InstalPleito> InstalPleitoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(LaboratorioPo.CodUsuarioNavigation))]
        public virtual ICollection<LaboratorioPo> LaboratorioPos { get; set; }
        [InverseProperty(nameof(MensagemUsuario.CodUsuarioNavigation))]
        public virtual ICollection<MensagemUsuario> MensagemUsuarios { get; set; }
        [InverseProperty(nameof(MotivoReaberturaO.CodUsuarioCadastroNavigation))]
        public virtual ICollection<MotivoReaberturaO> MotivoReaberturaOs { get; set; }
        [InverseProperty(nameof(NotaFaturamentoSicredi.CodUsuarioCadastroNavigation))]
        public virtual ICollection<NotaFaturamentoSicredi> NotaFaturamentoSicredis { get; set; }
        [InverseProperty(nameof(NumeroSerieControleEquipamento.CodUsuarioCadastroNavigation))]
        public virtual ICollection<NumeroSerieControleEquipamento> NumeroSerieControleEquipamentos { get; set; }
        [InverseProperty(nameof(NumeroSerieControleTitulo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<NumeroSerieControleTitulo> NumeroSerieControleTitulos { get; set; }
        [InverseProperty(nameof(OscanceladaPossicredi.CodUsuarioCancelamentoNavigation))]
        public virtual ICollection<OscanceladaPossicredi> OscanceladaPossicredis { get; set; }
        [InverseProperty(nameof(OscopiaPo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<OscopiaPo> OscopiaPos { get; set; }
        [InverseProperty(nameof(OshistPo.CodUsuarioNavigation))]
        public virtual ICollection<OshistPo> OshistPos { get; set; }
        [InverseProperty(nameof(Osposimagen.CodUsuarioCadastroNavigation))]
        public virtual ICollection<Osposimagen> Osposimagens { get; set; }
        [InverseProperty(nameof(Osreabertura.CodUsuarioNavigation))]
        public virtual ICollection<Osreabertura> Osreaberturas { get; set; }
        [InverseProperty(nameof(OsressarcirCobrancaSicrediPo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<OsressarcirCobrancaSicrediPo> OsressarcirCobrancaSicrediPos { get; set; }
        [InverseProperty(nameof(PagamentosPo.CodUsuarioNavigation))]
        public virtual ICollection<PagamentosPo> PagamentosPos { get; set; }
        [InverseProperty(nameof(Pai.CodUsuarioCadNavigation))]
        public virtual ICollection<Pai> PaiCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(Pai.CodUsuarioManutNavigation))]
        public virtual ICollection<Pai> PaiCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(PatrimonioPo.CodUsuarioNavigation))]
        public virtual ICollection<PatrimonioPo> PatrimonioPos { get; set; }
        [InverseProperty(nameof(PedidoInvoice.CodUsuarioResponsavelNavigation))]
        public virtual ICollection<PedidoInvoice> PedidoInvoices { get; set; }
        [InverseProperty(nameof(PontoColaborador.CodUsuarioNavigation))]
        public virtual ICollection<PontoColaborador> PontoColaboradors { get; set; }
        [InverseProperty(nameof(PontoMovel.CodUsuarioNavigation))]
        public virtual ICollection<PontoMovel> PontoMovels { get; set; }
        [InverseProperty(nameof(PontoSobreAviso.CodUsuarioPontoNavigation))]
        public virtual ICollection<PontoSobreAviso> PontoSobreAvisos { get; set; }
        [InverseProperty(nameof(PontoUsuario.CodUsuarioNavigation))]
        public virtual ICollection<PontoUsuario> PontoUsuarios { get; set; }
        [InverseProperty(nameof(Ponto.CodUsuarioNavigation))]
        public virtual ICollection<Ponto> Pontos { get; set; }
        [InverseProperty(nameof(PosvendaBanrisul.CodUsuarioCadastroNavigation))]
        public virtual ICollection<PosvendaBanrisul> PosvendaBanrisulCodUsuarioCadastroNavigations { get; set; }
        [InverseProperty(nameof(PosvendaBanrisul.CodUsuarioDesativacaoNavigation))]
        public virtual ICollection<PosvendaBanrisul> PosvendaBanrisulCodUsuarioDesativacaoNavigations { get; set; }
        [InverseProperty(nameof(ProducaoPo.CodUsuarioCadastroNavigation))]
        public virtual ICollection<ProducaoPo> ProducaoPos { get; set; }
        [InverseProperty(nameof(RelatorioApresentacao.CodUsuarioCadNavigation))]
        public virtual ICollection<RelatorioApresentacao> RelatorioApresentacaoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(RelatorioApresentacao.CodUsuarioManutNavigation))]
        public virtual ICollection<RelatorioApresentacao> RelatorioApresentacaoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(Relatorio.CodUsuarioCadNavigation))]
        public virtual ICollection<Relatorio> RelatorioCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(Relatorio.CodUsuarioManutNavigation))]
        public virtual ICollection<Relatorio> RelatorioCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(RelatorioPerfil.CodUsuarioCadNavigation))]
        public virtual ICollection<RelatorioPerfil> RelatorioPerfils { get; set; }
        [InverseProperty(nameof(RelatorioUsuario.CodUsuarioCadNavigation))]
        public virtual ICollection<RelatorioUsuario> RelatorioUsuarioCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(RelatorioUsuario.CodUsuarioNavigation))]
        public virtual ICollection<RelatorioUsuario> RelatorioUsuarioCodUsuarioNavigations { get; set; }
        [InverseProperty(nameof(RelatorioVisao.CodUsuarioCadNavigation))]
        public virtual ICollection<RelatorioVisao> RelatorioVisaoCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(RelatorioVisao.CodUsuarioManutNavigation))]
        public virtual ICollection<RelatorioVisao> RelatorioVisaoCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(RelatorioVisaoUsuario.CodUsuarioCadNavigation))]
        public virtual ICollection<RelatorioVisaoUsuario> RelatorioVisaoUsuarioCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(RelatorioVisaoUsuario.CodUsuarioNavigation))]
        public virtual ICollection<RelatorioVisaoUsuario> RelatorioVisaoUsuarioCodUsuarioNavigations { get; set; }
        [InverseProperty(nameof(SessaoUsuario.CodUsuarioNavigation))]
        public virtual ICollection<SessaoUsuario> SessaoUsuarios { get; set; }
        [InverseProperty(nameof(TarefaNotificacao.CodUsuarioNavigation))]
        public virtual ICollection<TarefaNotificacao> TarefaNotificacaos { get; set; }
        [InverseProperty(nameof(Tarefa.CodUsuarioNavigation))]
        public virtual ICollection<Tarefa> Tarefas { get; set; }
        [InverseProperty(nameof(TecnicoEquipamento.CodUsuarioNavigation))]
        public virtual ICollection<TecnicoEquipamento> TecnicoEquipamentos { get; set; }
        [InverseProperty(nameof(TipoUsuario.CodUsuarioCadastroNavigation))]
        public virtual ICollection<TipoUsuario> TipoUsuarios { get; set; }
        [InverseProperty(nameof(Uf.CodUsuarioCadNavigation))]
        public virtual ICollection<Uf> UfCodUsuarioCadNavigations { get; set; }
        [InverseProperty(nameof(Uf.CodUsuarioManutNavigation))]
        public virtual ICollection<Uf> UfCodUsuarioManutNavigations { get; set; }
        [InverseProperty(nameof(UsuarioDashboard.CodUsuarioNavigation))]
        public virtual ICollection<UsuarioDashboard> UsuarioDashboards { get; set; }
        [InverseProperty(nameof(UsuarioMenu.CodUsuarioNavigation))]
        public virtual ICollection<UsuarioMenu> UsuarioMenus { get; set; }
        [InverseProperty(nameof(UsuarioNovaSenha.CodUsuarioNavigation))]
        public virtual ICollection<UsuarioNovaSenha> UsuarioNovaSenhas { get; set; }
        [InverseProperty(nameof(UsuarioTipoUsuario.CodUsuarioCadastroNavigation))]
        public virtual ICollection<UsuarioTipoUsuario> UsuarioTipoUsuarioCodUsuarioCadastroNavigations { get; set; }
        [InverseProperty(nameof(ValoresAutorizadaPo.CodUsuarioNavigation))]
        public virtual ICollection<ValoresAutorizadaPo> ValoresAutorizadaPos { get; set; }
        [InverseProperty(nameof(VioladosPosbanrisul.CodUsuarioNavigation))]
        public virtual ICollection<VioladosPosbanrisul> VioladosPosbanrisuls { get; set; }
    }
}
