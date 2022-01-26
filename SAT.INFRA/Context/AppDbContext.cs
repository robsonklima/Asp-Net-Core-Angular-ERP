using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Mapping;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<OrdemServico> OrdemServico { get; set; }
        public DbSet<RelatorioAtendimento> RelatorioAtendimento { get; set; }
        public DbSet<RelatorioAtendimentoDetalhe> RelatorioAtendimentoDetalhe { get; set; }
        public DbSet<RelatorioAtendimentoDetalhePeca> RelatorioAtendimentoDetalhePeca { get; set; }
        public DbSet<StatusServico> StatusServico { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Sequencia> Sequencia { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<MotivoAgendamento> MotivoAgendamento { get; set; }
        public DbSet<TipoIntervencao> TipoIntervencao { get; set; }
        public DbSet<TipoServico> TipoServico { get; set; }
        public DbSet<LocalAtendimento> LocalAtendimento { get; set; }
        public DbSet<Equipamento> Equipamento { get; set; }
        public DbSet<DespesaConfiguracao> DespesaConfiguracao { get; set; }
        public DbSet<DespesaItemAlerta> DespesaItemAlerta { get; set; }
        public DbSet<GrupoEquipamento> GrupoEquipamento { get; set; }
        public DbSet<TipoEquipamento> TipoEquipamento { get; set; }
        public DbSet<EquipamentoContrato> EquipamentoContrato { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<ContratoEquipamentoData> ContratoEquipamentoData { get; set; }
        public DbSet<ContratoReajuste> ContratoReajuste { get; set; }
        public DbSet<Filial> Filial { get; set; }
        public DbSet<Defeito> Defeito { get; set; }
        public DbSet<Acao> Acao { get; set; }
        public DbSet<MediaAtendimentoTecnico> MediaAtendimentoTecnico { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<DespesaCartaoCombustivel> DespesaCartaoCombustivel { get; set; }
        public DbSet<Autorizada> Autorizada { get; set; }
        public DbSet<AcordoNivelServico> AcordoNivelServico { get; set; }
        public DbSet<Navegacao> Navegacao { get; set; }
        public DbSet<NavegacaoConfiguracao> NavegacaoConfiguracao { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<UnidadeFederativa> UnidadeFederativa { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Peca> Peca { get; set; }
        public DbSet<Transportadora> Transportadora { get; set; }
        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<Causa> Causa { get; set; }
        public DbSet<GrupoCausa> GrupoCausa { get; set; }
        public DbSet<TipoCausa> TipoCausa { get; set; }
        public DbSet<TipoContrato> TipoContrato { get; set; }
        public DbSet<TipoIndiceReajuste> TipoIndiceReajuste { get; set; }
        public DbSet<RegiaoAutorizada> RegiaoAutorizada { get; set; }
        public DbSet<Localizacao> Localizacao { get; set; }
        public DbSet<ContratoEquipamento> ContratoEquipamento { get; set; }
        public DbSet<ContratoSLA> ContratoSLA { get; set; }
        public DbSet<AgendaTecnico> AgendaTecnico { get; set; }
        public DbSet<Geolocalizacao> Geolocalizacao { get; set; }
        public DbSet<DispBBCalcEquipamentoContrato> DispBBCalcEquipamentoContrato { get; set; }
        public DbSet<DispBBCriticidade> DispBBCriticidade { get; set; }
        public DbSet<DispBBRegiaoFilial> DispBBRegiaoFilial { get; set; }
        public DbSet<DispBBPercRegiao> DispBBPercRegiao { get; set; }
        public DbSet<DispBBDesvio> DispBBDesvio { get; set; }
        public DbSet<PontoMovel> PontoMovel { get; set; }
        public DbSet<PontoMovelTipoHorario> PontoMovelTipoHorario { get; set; }
        public DbSet<PontoPeriodo> PontoPeriodo { get; set; }
        public DbSet<PontoPeriodoIntervaloAcessoData> PontoPeriodoIntervaloAcessoData { get; set; }
        public DbSet<PontoPeriodoModoAprovacao> PontoPeriodoModoAprovacao { get; set; }
        public DbSet<PontoPeriodoStatus> PontoPeriodoStatus { get; set; }
        public DbSet<PontoPeriodoUsuario> PontoPeriodoUsuario { get; set; }
        public DbSet<PontoPeriodoUsuarioStatus> PontoPeriodoUsuarioStatus { get; set; }
        public DbSet<PontoSobreAviso> PontoSobreAviso { get; set; }
        public DbSet<PontoTipoHora> PontoTipoHora { get; set; }
        public DbSet<PontoUsuario> PontoUsuario { get; set; }
        public DbSet<PontoUsuarioData> PontoUsuarioData { get; set; }
        public DbSet<PontoUsuarioDataAdvertencia> PontoUsuarioDataAdvertencia { get; set; }
        public DbSet<PontoUsuarioDataControleAlteracaoAcesso> PontoUsuarioDataControleAlteracaoAcesso { get; set; }
        public DbSet<PontoUsuarioDataDivergencia> PontoUsuarioDataDivergencia { get; set; }
        public DbSet<PontoUsuarioDataDivergenciaObservacao> PontoUsuarioDataDivergenciaObservacao { get; set; }
        public DbSet<PontoUsuarioDataDivergenciaRAT> PontoUsuarioDataDivergenciaRAT { get; set; }
        public DbSet<PontoUsuarioDataJustificativaAlteracaoAcesso> PontoUsuarioDataJustificativaAlteracaoAcesso { get; set; }
        public DbSet<PontoUsuarioDataJustificativaValidacao> PontoUsuarioDataJustificativaValidacao { get; set; }
        public DbSet<PontoUsuarioDataModoAlteracaoAcesso> PontoUsuarioDataModoAlteracaoAcesso { get; set; }
        public DbSet<PontoUsuarioDataModoDivergencia> PontoUsuarioDataModoDivergencia { get; set; }
        public DbSet<PontoUsuarioDataMotivoDivergencia> PontoUsuarioDataMotivoDivergencia { get; set; }
        public DbSet<PontoUsuarioDataStatus> PontoUsuarioDataStatus { get; set; }
        public DbSet<PontoUsuarioDataStatusAcesso> PontoUsuarioDataStatusAcesso { get; set; }
        public DbSet<PontoUsuarioDataTipoAdvertencia> PontoUsuarioDataTipoAdvertencia { get; set; }
        public DbSet<PontoUsuarioDataValidacao> PontoUsuarioDataValidacao { get; set; }
        public DbSet<PontoUsuarioRejeicao> PontoUsuarioRejeicao { get; set; }
        public DbSet<PlantaoTecnico> PlantaoTecnico { get; set; }
        public DbSet<DespesaAdiantamentoPeriodo> DespesaAdiantamentoPeriodo { get; set; }
        public DbSet<DespesaAdiantamento> DespesaAdiantamento { get; set; }
        public DbSet<DespesaConfiguracaoCombustivel> DespesaConfiguracaoCombustivel { get; set; }
        public DbSet<DespesaItem> DespesaItem { get; set; }
        public DbSet<DespesaPeriodo> DespesaPeriodo { get; set; }
        public DbSet<DespesaProtocolo> DespesaProtocolo { get; set; }
        public DbSet<DespesaPeriodoTecnico> DespesaPeriodoTecnico { get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<DespesaTipo> DespesaTipo { get; set; }
        public DbSet<DespesaAdiantamentoTipo> DespesaAdiantamentoTipo { get; set; }
        public DbSet<DespesaCartaoCombustivelTecnico> DespesaCartaoCombustivelTecnico { get; set; }
        public DbSet<DespesaProtocoloPeriodoTecnico> DespesaProtocoloPeriodoTecnico { get; set; }
        public DbSet<TicketLogPedidoCredito> TicketLogPedidoCredito { get; set; }
        public DbSet<TecnicoConta> TecnicoConta { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<DashboardIndicadores> DashboardIndicadores { get; set; }
        public DbSet<Instalacao> Instalacao { get; set; }
        public DbSet<InstalacaoLote> InstalacaoLote { get; set; }
        public DbSet<InstalacaoRessalva> InstalacaoRessalva { get; set; }
        public DbSet<InstalacaoMotivoRes> InstalacaoMotivoRes { get; set; }
        public DbSet<Laudo> Laudo { get; set; }
        public DbSet<LaudoStatus> LaudoStatus { get; set; }
        public DbSet<LaudoSituacao> LaudoSituacao { get; set; }
        public DbSet<FiltroUsuario> FiltroUsuario { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<UsuarioDispositivo> UsuarioDispositivo { get; set; }
        public DbSet<OrdemServicoHistorico> OrdemServicoHistorico { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<OrcamentoMaterial> OrcamentoMaterial { get; set; }
        public DbSet<OrcamentoMotivo> OrcamentoMotivo { get; set; }
        public DbSet<OrcamentoDesconto> OrcamentoDesconto { get; set; }
        public DbSet<OrcamentoMaoDeObra> OrcamentoMaoDeObra { get; set; }
        public DbSet<OrcamentoOutroServico> OrcamentoOutroServico { get; set; }
        public DbSet<EnderecoFaturamentoNF> EnderecoFaturamentoNF { get; set; }
        public DbSet<EnderecoFaturamentoVinculado> EnderecoFaturamentoVinculado { get; set; }
        public DbSet<Monitoramento> Monitoramento { get; set; }
        public DbSet<MonitoramentoHistorico> MonitoramentoHistorico { get; set; }
        public DbSet<OrcamentoStatus> OrcamentoStatus { get; set; }
        public DbSet<OrcamentoDeslocamento> OrcamentoDeslocamento { get; set; }
        public DbSet<ImportacaoConfiguracao> ImportacaoConfiguracao { get; set; }
        public DbSet<ImportacaoTipo> ImportacaoTipo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sequencia>(new SequenciaMap().Configure);
            modelBuilder.Entity<ContratoEquipamento>(new ContratoEquipamentoMap().Configure);
            modelBuilder.Entity<ContratoSLA>(new ContratoSLAMap().Configure);
            modelBuilder.Entity<AgendaTecnico>(new AgendaTecnicoMap().Configure);
            modelBuilder.Entity<DespesaPeriodoTecnico>(new DespesaPeriodoTecnicoMap().Configure);
            modelBuilder.Entity<TicketLogPedidoCredito>(new TicketLogPedidoCreditoMap().Configure);
            modelBuilder.Entity<TicketLogUsuarioCartaoPlaca>(new TicketLogUsuarioCartaoPlacaMap().Configure);
            modelBuilder.Entity<DespesaCartaoCombustivel>(new DespesaCartaoCombustivelMap().Configure);
            modelBuilder.Entity<Tecnico>(new TecnicoMap().Configure);
            modelBuilder.Entity<DespesaCartaoCombustivelTecnico>(new DespesaCartaoCombustivelTecnicoMap().Configure);
            modelBuilder.Entity<TecnicoConta>(new TecnicoContaMap().Configure);
            modelBuilder.Entity<Instalacao>(new InstalacaoMap().Configure);
            modelBuilder.Entity<Laudo>(new LaudoMap().Configure);
            modelBuilder.Entity<LaudoStatus>(new LaudoStatusMap().Configure);
            modelBuilder.Entity<LaudoSituacao>(new LaudoSituacaoMap().Configure);
            modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
            modelBuilder.Entity<EquipamentoContrato>(new EquipamentoContratoMap().Configure);
            modelBuilder.Entity<UnidadeFederativa>(new UnidadeFederativaMap().Configure);
            modelBuilder.Entity<DispBBRegiao>(new DispBBRegiaoMap().Configure);
            modelBuilder.Entity<DispBBCalcEquipamentoContrato>(new DispBBCalcEquipamentoContratoMap().Configure);
            modelBuilder.Entity<RelatorioAtendimento>(new RelatorioAtendimentoMap().Configure);
            modelBuilder.Entity<Foto>(new FotoMap().Configure);
            modelBuilder.Entity<UsuarioDispositivo>(new UsuarioDispositivoMap().Configure);
            modelBuilder.Entity<OrdemServico>(new OrdemServicoMap().Configure);
            modelBuilder.Entity<OrdemServicoHistorico>(new OrdemServicoHistoricoMap().Configure);
            modelBuilder.Entity<Orcamento>(new OrcamentoMap().Configure);
            modelBuilder.Entity<OrcamentoMotivo>(new OrcamentoMotivoMap().Configure);
            modelBuilder.Entity<EnderecoFaturamentoVinculado>(new EnderecoFaturamentoVinculadoMap().Configure);
            modelBuilder.Entity<EnderecoFaturamentoNF>(new EnderecoFaturamentoNFMap().Configure);
            modelBuilder.Entity<OrcamentoMaterial>(new OrcamentoMaterialMap().Configure);
            modelBuilder.Entity<OrcamentoMaoDeObra>(new OrcamentoMaoDeObraMap().Configure);
            modelBuilder.Entity<OrcamentoOutroServico>(new OrcamentoOutroServicoMap().Configure);
            modelBuilder.Entity<OrcamentoDesconto>(new OrcamentoDescontoMap().Configure);
            modelBuilder.Entity<OrcamentoStatus>(new OrcamentoStatusMap().Configure);
            modelBuilder.Entity<OrcamentoDeslocamento>(new OrcamentoDeslocamentoMap().Configure);
            modelBuilder.Entity<Monitoramento>(new MonitoramentoMap().Configure);
            modelBuilder.Entity<MonitoramentoHistorico>(new MonitoramentoHistoricoMap().Configure);
            modelBuilder.Entity<OrcamentoISS>(new OrcamentoISSMap().Configure);
            modelBuilder.Entity<Filial>(new FilialMap().Configure);
            modelBuilder.Entity<Contrato>(new ContratoMap().Configure);
            modelBuilder.Entity<ContratoServico>(new ContratoServicoMap().Configure);
            modelBuilder.Entity<Peca>(new PecaMap().Configure);
            modelBuilder.Entity<ClientePeca>(new ClientePecaMap().Configure);
            modelBuilder.Entity<ClientePecaGenerica>(new ClientePecaGenericaMap().Configure);
            modelBuilder.Entity<ImportacaoConfiguracao>(new ImportacaoConfiguracaoMap().Configure);
            modelBuilder.Entity<ImportacaoTipo>(new ImportacaoTipoMap().Configure);

            modelBuilder.Entity<RegiaoAutorizada>()
                        .HasKey(ra => new { ra.CodFilial, ra.CodRegiao, ra.CodAutorizada });

            modelBuilder.Entity<NavegacaoConfiguracao>()
                        .HasOne<Perfil>(nc => nc.Perfil)
                        .WithMany(nc => nc.NavegacoesConfiguracao);

            modelBuilder.Entity<NavegacaoConfiguracao>()
                        .HasOne<Navegacao>(nc => nc.Navegacao)
                        .WithMany(nc => nc.NavegacoesConfiguracao);

            modelBuilder.Entity<DespesaPeriodoTecnico>()
                        .HasKey(ra => new { ra.CodTecnico, ra.CodDespesaPeriodo });

            modelBuilder.Entity<ContratoEquipamento>()
                        .HasKey(e => new { e.CodContrato, e.CodEquip });

            modelBuilder.Entity<DispBBEquipamentoContrato>()
                .HasKey(e => e.CodDispBBEquipamentoContrato);

            modelBuilder.Entity<DespesaProtocoloPeriodoTecnico>()
                        .HasKey(ra => new { ra.CodDespesaProtocolo, ra.CodDespesaPeriodoTecnico });

            modelBuilder.Entity<DespesaProtocoloPeriodoTecnico>()
                        .HasOne(p => p.DespesaPeriodoTecnico)
                        .WithOne(p => p.DespesaProtocoloPeriodoTecnico)
                        .HasForeignKey<DespesaPeriodoTecnico>("CodDespesaPeriodoTecnico")
                        .HasPrincipalKey<DespesaProtocoloPeriodoTecnico>("CodDespesaPeriodoTecnico");
        }
    }
}