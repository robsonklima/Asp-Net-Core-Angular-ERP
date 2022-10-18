﻿
using Autofac;
using SAT.INFRA.Interfaces;
using SAT.INFRA.Repository;
using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;

namespace SAT.IOC;

public class ConfigurationIOC
{
    public static void Load(ContainerBuilder builder)
    {
        #region Repositories
        builder.RegisterType<OrdemServicoRepository>().As<IOrdemServicoRepository>();
        builder.RegisterType<RelatorioAtendimentoRepository>().As<IRelatorioAtendimentoRepository>();
        builder.RegisterType<RelatorioAtendimentoRepository>().As<IRelatorioAtendimentoRepository>();
        builder.RegisterType<StatusServicoRepository>().As<IStatusServicoRepository>();
        builder.RegisterType<LocalAtendimentoRepository>().As<ILocalAtendimentoRepository>();
        builder.RegisterType<UnidadeFederativaRepository>().As<IUnidadeFederativaRepository>();
        builder.RegisterType<UsuarioRepository>().As<IUsuarioRepository>();
        builder.RegisterType<ClienteRepository>().As<IClienteRepository>();
        builder.RegisterType<TipoIntervencaoRepository>().As<ITipoIntervencaoRepository>();
        builder.RegisterType<FilialRepository>().As<IFilialRepository>();
        builder.RegisterType<DefeitoRepository>().As<IDefeitoRepository>();
        builder.RegisterType<AcaoRepository>().As<IAcaoRepository>();
        builder.RegisterType<SequenciaRepository>().As<ISequenciaRepository>();
        builder.RegisterType<EquipamentoRepository>().As<IEquipamentoRepository>();
        builder.RegisterType<EquipamentoContratoRepository>().As<IEquipamentoContratoRepository>().SingleInstance();
        builder.RegisterType<ContratoRepository>().As<IContratoRepository>();
        builder.RegisterType<TipoEquipamentoRepository>().As<ITipoEquipamentoRepository>();
        builder.RegisterType<TipoServicoRepository>().As<ITipoServicoRepository>();
        builder.RegisterType<RegiaoRepository>().As<IRegiaoRepository>();
        builder.RegisterType<AutorizadaRepository>().As<IAutorizadaRepository>();
        builder.RegisterType<AuditoriaRepository>().As<IAuditoriaRepository>();
        builder.RegisterType<AuditoriaFotoRepository>().As<IAuditoriaFotoRepository>();
        builder.RegisterType<AuditoriaStatusRepository>().As<IAuditoriaStatusRepository>();
        builder.RegisterType<AuditoriaVeiculoRepository>().As<IAuditoriaVeiculoRepository>();
        builder.RegisterType<AuditoriaVeiculoAcessorioRepository>().As<IAuditoriaVeiculoAcessorioRepository>();
        builder.RegisterType<AuditoriaVeiculoTanqueRepository>().As<IAuditoriaVeiculoTanqueRepository>();
        builder.RegisterType<PerfilRepository>().As<IPerfilRepository>();
        builder.RegisterType<PaisRepository>().As<IPaisRepository>();
        builder.RegisterType<CidadeRepository>().As<ICidadeRepository>();
        builder.RegisterType<PecaRepository>().As<IPecaRepository>();
        builder.RegisterType<GrupoCausaRepository>().As<IGrupoCausaRepository>();
        builder.RegisterType<TipoCausaRepository>().As<ITipoCausaRepository>();
        builder.RegisterType<TipoContratoRepository>().As<ITipoContratoRepository>();
        builder.RegisterType<TipoIndiceReajusteRepository>().As<ITipoIndiceReajusteRepository>();
        builder.RegisterType<GrupoEquipamentoRepository>().As<IGrupoEquipamentoRepository>();
        builder.RegisterType<AcordoNivelServicoRepository>().As<IAcordoNivelServicoRepository>();
        builder.RegisterType<RelatorioAtendimentoDetalheRepository>().As<IRelatorioAtendimentoDetalheRepository>();
        builder.RegisterType<RelatorioAtendimentoDetalhePecaRepository>().As<IRelatorioAtendimentoDetalhePecaRepository>();
        builder.RegisterType<TransportadoraRepository>().As<ITransportadoraRepository>();
        builder.RegisterType<TecnicoRepository>().As<ITecnicoRepository>();
        builder.RegisterType<FeriadoRepository>().As<IFeriadoRepository>();
        builder.RegisterType<RegiaoAutorizadaRepository>().As<IRegiaoAutorizadaRepository>();
        builder.RegisterType<CausaRepository>().As<ICausaRepository>();
        builder.RegisterType<DespesaCartaoCombustivelRepository>().As<IDespesaCartaoCombustivelRepository>();
        builder.RegisterType<AgendamentoRepository>().As<IAgendamentoRepository>();
        builder.RegisterType<MotivoAgendamentoRepository>().As<IMotivoAgendamentoRepository>();
        builder.RegisterType<ContratoEquipamentoRepository>().As<IContratoEquipamentoRepository>();
        builder.RegisterType<ContratoEquipamentoDataRepository>().As<IContratoEquipamentoDataRepository>();
        builder.RegisterType<ContratoSLARepository>().As<IContratoSLARepository>();
        builder.RegisterType<ContratoServicoRepository>().As<IContratoServicoRepository>();
        builder.RegisterType<ContratoReajusteRepository>().As<IContratoReajusteRepository>();
        builder.RegisterType<ContratoReajusteRepository>().As<IContratoReajusteRepository>();
        builder.RegisterType<AgendaTecnicoRepository>().As<IAgendaTecnicoRepository>();
        builder.RegisterType<PontoUsuarioRepository>().As<IPontoUsuarioRepository>();
        builder.RegisterType<PontoPeriodoRepository>().As<IPontoPeriodoRepository>();
        builder.RegisterType<PontoPeriodoStatusRepository>().As<IPontoPeriodoStatusRepository>();
        builder.RegisterType<PontoPeriodoModoAprovacaoRepository>().As<IPontoPeriodoModoAprovacaoRepository>();
        builder.RegisterType<PontoPeriodoIntervaloAcessoDataRepository>().As<IPontoPeriodoIntervaloAcessoDataRepository>();
        builder.RegisterType<PontoUsuarioDataRepository>().As<IPontoUsuarioDataRepository>();
        builder.RegisterType<PontoUsuarioDataMotivoDivergenciaRepository>().As<IPontoUsuarioDataMotivoDivergenciaRepository>();
        builder.RegisterType<PontoUsuarioDataTipoAdvertenciaRepository>().As<IPontoUsuarioDataTipoAdvertenciaRepository>();
        builder.RegisterType<PontoUsuarioDataDivergenciaRepository>().As<IPontoUsuarioDataDivergenciaRepository>();
        builder.RegisterType<DispBBCriticidadeRepository>().As<IDispBBCriticidadeRepository>();
        builder.RegisterType<DispBBRegiaoFilialRepository>().As<IDispBBRegiaoFilialRepository>();
        builder.RegisterType<DispBBPercRegiaoRepository>().As<IDispBBPercRegiaoRepository>();
        builder.RegisterType<DispBBDesvioRepository>().As<IDispBBDesvioRepository>();
        builder.RegisterType<DespesaRepository>().As<IDespesaRepository>();
        builder.RegisterType<DespesaItemRepository>().As<IDespesaItemRepository>();
        builder.RegisterType<DespesaPeriodoRepository>().As<IDespesaPeriodoRepository>();
        builder.RegisterType<DespesaConfiguracaoCombustivelRepository>().As<IDespesaConfiguracaoCombustivelRepository>();
        builder.RegisterType<DespesaAdiantamentoPeriodoRepository>().As<IDespesaAdiantamentoPeriodoRepository>();
        builder.RegisterType<DespesaAdiantamentoRepository>().As<IDespesaAdiantamentoRepository>();
        builder.RegisterType<DespesaAdiantamentoTipoRepository>().As<IDespesaAdiantamentoTipoRepository>();
        builder.RegisterType<DespesaTipoRepository>().As<IDespesaTipoRepository>();
        builder.RegisterType<DespesaProtocoloRepository>().As<IDespesaProtocoloRepository>();
        builder.RegisterType<DespesaPeriodoTecnicoRepository>().As<IDespesaPeriodoTecnicoRepository>();
        builder.RegisterType<DashboardRepository>().As<IDashboardRepository>();
        builder.RegisterType<DespesaProtocoloPeriodoTecnicoRepository>().As<IDespesaProtocoloPeriodoTecnicoRepository>();
        builder.RegisterType<DespesaCartaoCombustivelTecnicoRepository>().As<IDespesaCartaoCombustivelTecnicoRepository>();
        builder.RegisterType<DespesaConfiguracaoRepository>().As<IDespesaConfiguracaoRepository>();
        builder.RegisterType<DespesaItemAlertaRepository>().As<IDespesaItemAlertaRepository>();
        builder.RegisterType<TicketLogPedidoCreditoRepository>().As<ITicketLogPedidoCreditoRepository>();
        builder.RegisterType<TurnoRepository>().As<ITurnoRepository>();
        builder.RegisterType<InstalacaoRepository>().As<IInstalacaoRepository>();
        builder.RegisterType<InstalacaoLoteRepository>().As<IInstalacaoLoteRepository>();
        builder.RegisterType<FiltroRepository>().As<IFiltroRepository>();
        builder.RegisterType<NotificacaoRepository>().As<INotificacaoRepository>();
        builder.RegisterType<DispBBCalcEquipamentoContratoRepository>().As<IDispBBCalcEquipamentoContratoRepository>();
        builder.RegisterType<FotoRepository>().As<IFotoRepository>();
        builder.RegisterType<UsuarioDispositivoRepository>().As<IUsuarioDispositivoRepository>();
        builder.RegisterType<OrdemServicoHistoricoRepository>().As<IOrdemServicoHistoricoRepository>();
        builder.RegisterType<MediaAtendimentoTecnicoRepository>().As<IMediaAtendimentoTecnicoRepository>();
        builder.RegisterType<PontoPeriodoUsuarioRepository>().As<IPontoPeriodoUsuarioRepository>();
        builder.RegisterType<OrcamentoRepository>().As<IOrcamentoRepository>();
        builder.RegisterType<OrcamentoMotivoRepository>().As<IOrcamentoMotivoRepository>();
        builder.RegisterType<OrcamentoOutroServicoRepository>().As<IOrcamentoOutroServicoRepository>();
        builder.RegisterType<OrcamentoMaterialRepository>().As<IOrcamentoMaterialRepository>();
        builder.RegisterType<OrcamentoMaoDeObraRepository>().As<IOrcamentoMaoDeObraRepository>();
        builder.RegisterType<OrcamentoMotivoRepository>().As<IOrcamentoMotivoRepository>();
        builder.RegisterType<MonitoramentoRepository>().As<IMonitoramentoRepository>();
        builder.RegisterType<MonitoramentoHistoricoRepository>().As<IMonitoramentoHistoricoRepository>();
        builder.RegisterType<LaudoRepository>().As<ILaudoRepository>();
        builder.RegisterType<OrcamentoStatusRepository>().As<IOrcamentoStatusRepository>();
        builder.RegisterType<OrcamentoDeslocamentoRepository>().As<IOrcamentoDeslocamentoRepository>();
        builder.RegisterType<OrcamentoDescontoRepository>().As<IOrcamentoDescontoRepository>();
        builder.RegisterType<LocalEnvioNFFaturamentoRepository>().As<ILocalEnvioNFFaturamentoRepository>();
        builder.RegisterType<LocalEnvioNFFaturamentoVinculadoRepository>().As<ILocalEnvioNFFaturamentoVinculadoRepository>();
        builder.RegisterType<IntencaoRepository>().As<IIntencaoRepository>();
        builder.RegisterType<VersaoRepository>().As<IVersaoRepository>();
        builder.RegisterType<CargoRepository>().As<ICargoRepository>();
        builder.RegisterType<PlantaoTecnicoRepository>().As<IPlantaoTecnicoRepository>();
        builder.RegisterType<PlantaoTecnicoRegiaoRepository>().As<IPlantaoTecnicoRegiaoRepository>();
        builder.RegisterType<PlantaoTecnicoClienteRepository>().As<IPlantaoTecnicoClienteRepository>();
        builder.RegisterType<FormaPagamentoRepository>().As<IFormaPagamentoRepository>();
        builder.RegisterType<MoedaRepository>().As<IMoedaRepository>();
        builder.RegisterType<PecaListaRepository>().As<IPecaListaRepository>();
        builder.RegisterType<PecaStatusRepository>().As<IPecaStatusRepository>();
        builder.RegisterType<TipoFreteRepository>().As<ITipoFreteRepository>();
        builder.RegisterType<ClienteBancadaRepository>().As<IClienteBancadaRepository>();
        builder.RegisterType<FerramentaTecnicoRepository>().As<IFerramentaTecnicoRepository>();
        builder.RegisterType<LiderTecnicoRepository>().As<ILiderTecnicoRepository>();
        builder.RegisterType<BancadaListaRepository>().As<IBancadaListaRepository>();
        builder.RegisterType<AcaoComponenteRepository>().As<IAcaoComponenteRepository>();
        builder.RegisterType<DefeitoComponenteRepository>().As<IDefeitoComponenteRepository>();
        builder.RegisterType<EquipamentoModuloRepository>().As<IEquipamentoModuloRepository>();
        builder.RegisterType<ClientePecaRepository>().As<IClientePecaRepository>();
        builder.RegisterType<ClientePecaGenericaRepository>().As<IClientePecaGenericaRepository>();
        builder.RegisterType<ImportacaoTipoRepository>().As<IImportacaoTipoRepository>();
        builder.RegisterType<ImportacaoConfiguracaoRepository>().As<IImportacaoConfiguracaoRepository>();
        builder.RegisterType<CheckinCheckoutRepository>().As<ICheckinCheckoutRepository>();
        builder.RegisterType<TicketRepository>().As<ITicketRepository>();
        builder.RegisterType<TicketModuloRepository>().As<ITicketModuloRepository>();
        builder.RegisterType<TicketStatusRepository>().As<ITicketStatusRepository>();
        builder.RegisterType<TicketAtendimentoRepository>().As<ITicketAtendimentoRepository>();
        builder.RegisterType<TicketPrioridadeRepository>().As<ITicketPrioridadeRepository>();
        builder.RegisterType<TicketClassificacaoRepository>().As<ITicketClassificacaoRepository>();
        builder.RegisterType<SatTaskRepository>().As<ISatTaskRepository>();
        builder.RegisterType<DispBBBloqueioOSRepository>().As<IDispBBBloqueioOSRepository>();
        builder.RegisterType<InstalacaoNFVendaRepository>().As<IInstalacaoNFVendaRepository>();
        builder.RegisterType<IntegracaoFinanceiroRepository>().As<IIntegracaoFinanceiroRepository>();
        builder.RegisterType<PosVendaRepository>().As<IPosVendaRepository>();
        builder.RegisterType<OrcFormaPagamentoRepository>().As<IOrcFormaPagamentoRepository>();
        builder.RegisterType<OrcDadosBancariosRepository>().As<IOrcDadosBancariosRepository>();
        builder.RegisterType<OrcamentoFaturamentoRepository>().As<IOrcamentoFaturamentoRepository>();
        builder.RegisterType<ConferenciaRepository>().As<IConferenciaRepository>();
        builder.RegisterType<ConferenciaParticipanteRepository>().As<IConferenciaParticipanteRepository>();
        builder.RegisterType<MensagemTecnicoRepository>().As<IMensagemTecnicoRepository>();
        builder.RegisterType<ArquivoBanrisulRepository>().As<IArquivoBanrisulRepository>();
        builder.RegisterType<OrdemServicoSTNRepository>().As<IOrdemServicoSTNRepository>();
        builder.RegisterType<ProtocoloSTNRepository>().As<IProtocoloSTNRepository>();
        builder.RegisterType<IntegracaoRepository>().As<IIntegracaoRepository>();
        builder.RegisterType<LaboratorioRepository>().As<ILaboratorioRepository>();
        
        #endregion

        #region Services
        builder.RegisterType<ImportacaoService>().As<IImportacaoService>();
        builder.RegisterType<ImportacaoConfiguracaoService>().As<IImportacaoConfiguracaoService>();
        builder.RegisterType<ImportacaoTipoService>().As<IImportacaoTipoService>();
        builder.RegisterType<ExportacaoService>().As<IExportacaoService>();
        builder.RegisterType<DispBBBloqueioOSService>().As<IDispBBBloqueioOSService>();
        builder.RegisterType<AcaoService>().As<IAcaoService>();
        builder.RegisterType<AcordoNivelServicoService>().As<IAcordoNivelServicoService>();
        builder.RegisterType<AutorizadaService>().As<IAutorizadaService>();
        builder.RegisterType<AuditoriaService>().As<IAuditoriaService>();
        builder.RegisterType<AuditoriaFotoService>().As<IAuditoriaFotoService>();
        builder.RegisterType<AuditoriaStatusService>().As<IAuditoriaStatusService>();
        builder.RegisterType<AuditoriaVeiculoService>().As<IAuditoriaVeiculoService>();
        builder.RegisterType<AuditoriaVeiculoAcessorioService>().As<IAuditoriaVeiculoAcessorioService>();
        builder.RegisterType<AuditoriaVeiculoTanqueService>().As<IAuditoriaVeiculoTanqueService>();
        builder.RegisterType<CausaService>().As<ICausaService>();
        builder.RegisterType<CidadeService>().As<ICidadeService>();
        builder.RegisterType<ClienteService>().As<IClienteService>();
        builder.RegisterType<ContratoEquipamentoService>().As<IContratoEquipamentoService>();
        builder.RegisterType<ContratoEquipamentoDataService>().As<IContratoEquipamentoDataService>();
        builder.RegisterType<ContratoService>().As<IContratoService>();
        builder.RegisterType<ContratoSLAService>().As<IContratoSLAService>();
        builder.RegisterType<ContratoServicoService>().As<IContratoServicoService>();
        builder.RegisterType<ContratoReajusteService>().As<IContratoReajusteService>();
        builder.RegisterType<DefeitoService>().As<IDefeitoService>();
        builder.RegisterType<DespesaCartaoCombustivelService>().As<IDespesaCartaoCombustivelService>();
        builder.RegisterType<EquipamentoContratoService>().As<IEquipamentoContratoService>();
        builder.RegisterType<EquipamentoService>().As<IEquipamentoService>();
        builder.RegisterType<FeriadoService>().As<IFeriadoService>();
        builder.RegisterType<GrupoCausaService>().As<IGrupoCausaService>();
        builder.RegisterType<GrupoEquipamentoService>().As<IGrupoEquipamentoService>();
        builder.RegisterType<LocalAtendimentoService>().As<ILocalAtendimentoService>();
        builder.RegisterType<MotivoAgendamentoService>().As<IMotivoAgendamentoService>();
        builder.RegisterType<PaisService>().As<IPaisService>();
        builder.RegisterType<PecaService>().As<IPecaService>();
        builder.RegisterType<PecaStatusService>().As<IPecaStatusService>();
        builder.RegisterType<FilialService>().As<IFilialService>();
        builder.RegisterType<PerfilService>().As<IPerfilService>();
        builder.RegisterType<RegiaoAutorizadaService>().As<IRegiaoAutorizadaService>();
        builder.RegisterType<RegiaoService>().As<IRegiaoService>();
        builder.RegisterType<RelatorioAtendimentoDetalhePecaService>().As<IRelatorioAtendimentoDetalhePecaService>();
        builder.RegisterType<RelatorioAtendimentoDetalheService>().As<IRelatorioAtendimentoDetalheService>();
        builder.RegisterType<RelatorioAtendimentoService>().As<IRelatorioAtendimentoService>();
        builder.RegisterType<StatusServicoService>().As<IStatusServicoService>();
        builder.RegisterType<TecnicoService>().As<ITecnicoService>();
        builder.RegisterType<TipoCausaService>().As<ITipoCausaService>();
        builder.RegisterType<TipoEquipamentoService>().As<ITipoEquipamentoService>();
        builder.RegisterType<TipoIntervencaoService>().As<ITipoIntervencaoService>();
        builder.RegisterType<TipoServicoService>().As<ITipoServicoService>();
        builder.RegisterType<TipoContratoService>().As<ITipoContratoService>();
        builder.RegisterType<TipoIndiceReajusteService>().As<ITipoIndiceReajusteService>();
        builder.RegisterType<TransportadoraService>().As<ITransportadoraService>();
        builder.RegisterType<UnidadeFederativaService>().As<IUnidadeFederativaService>();
        builder.RegisterType<UsuarioService>().As<IUsuarioService>();
        builder.RegisterType<UsuarioService>().As<IUsuarioService>();
        builder.RegisterType<OrdemServicoService>().As<IOrdemServicoService>();
        builder.RegisterType<OrdemServicoAlertaService>().As<IOrdemServicoAlertaService>();
        builder.RegisterType<AgendaTecnicoService>().As<IAgendaTecnicoService>();
        builder.RegisterType<AgendamentoService>().As<IAgendamentoService>();
        builder.RegisterType<GeolocalizacaoService>().As<IGeolocalizacaoService>();
        builder.RegisterType<PontoUsuarioService>().As<IPontoUsuarioService>();
        builder.RegisterType<PontoPeriodoService>().As<IPontoPeriodoService>();
        builder.RegisterType<PontoPeriodoStatusService>().As<IPontoPeriodoStatusService>();
        builder.RegisterType<PontoPeriodoModoAprovacaoService>().As<IPontoPeriodoModoAprovacaoService>();
        builder.RegisterType<PontoPeriodoIntervaloAcessoDataService>().As<IPontoPeriodoIntervaloAcessoDataService>();
        builder.RegisterType<PontoUsuarioDataService>().As<IPontoUsuarioDataService>();
        builder.RegisterType<PontoUsuarioDataMotivoDivergenciaService>().As<IPontoUsuarioDataMotivoDivergenciaService>();
        builder.RegisterType<PontoUsuarioDataTipoAdvertenciaService>().As<IPontoUsuarioDataTipoAdvertenciaService>();
        builder.RegisterType<PontoUsuarioDataDivergenciaService>().As<IPontoUsuarioDataDivergenciaService>();
        builder.RegisterType<MonitoramentoService>().As<IMonitoramentoService>();
        builder.RegisterType<TurnoService>().As<ITurnoService>();
        builder.RegisterType<FotoService>().As<IFotoService>();
        builder.RegisterType<FiltroService>().As<IFiltroService>();
        builder.RegisterType<UsuarioDispositivoService>().As<IUsuarioDispositivoService>();
        builder.RegisterType<OrdemServicoHistoricoService>().As<IOrdemServicoHistoricoService>();
        builder.RegisterType<EmailService>().As<IEmailService>();
        builder.RegisterType<DespesaAdiantamentoService>().As<IDespesaAdiantamentoService>();
        builder.RegisterType<DespesaAdiantamentoTipoService>().As<IDespesaAdiantamentoTipoService>();
        builder.RegisterType<DespesaAdiantamentoPeriodoService>().As<IDespesaAdiantamentoPeriodoService>();
        builder.RegisterType<DespesaConfiguracaoCombustivelService>().As<IDespesaConfiguracaoCombustivelService>();
        builder.RegisterType<DespesaService>().As<IDespesaService>();
        builder.RegisterType<DespesaItemService>().As<IDespesaItemService>();
        builder.RegisterType<DespesaPeriodoService>().As<IDespesaPeriodoService>();
        builder.RegisterType<DespesaProtocoloService>().As<IDespesaProtocoloService>();
        builder.RegisterType<DespesaTipoService>().As<IDespesaTipoService>();
        builder.RegisterType<DespesaPeriodoTecnicoService>().As<IDespesaPeriodoTecnicoService>();
        builder.RegisterType<DashboardService>().As<IDashboardService>();
        builder.RegisterType<DespesaProtocoloPeriodoTecnicoService>().As<IDespesaProtocoloPeriodoTecnicoService>();
        builder.RegisterType<DespesaCartaoCombustivelTecnicoService>().As<IDespesaCartaoCombustivelTecnicoService>();
        builder.RegisterType<DespesaConfiguracaoService>().As<IDespesaConfiguracaoService>();
        builder.RegisterType<DespesaItemAlertaService>().As<IDespesaItemAlertaService>();
        builder.RegisterType<TicketLogPedidoCreditoService>().As<ITicketLogPedidoCreditoService>();
        builder.RegisterType<InstalacaoService>().As<IInstalacaoService>();
        builder.RegisterType<InstalacaoLoteService>().As<IInstalacaoLoteService>();
        builder.RegisterType<NotificacaoService>().As<INotificacaoService>();
        builder.RegisterType<PontoPeriodoUsuarioService>().As<IPontoPeriodoUsuarioService>();
        builder.RegisterType<OrcamentoService>().As<IOrcamentoService>();
        builder.RegisterType<OrcamentoMotivoService>().As<IOrcamentoMotivoService>();
        builder.RegisterType<OrcamentoOutroServicoService>().As<IOrcamentoOutroServicoService>();
        builder.RegisterType<OrcamentoMaterialService>().As<IOrcamentoMaterialService>();
        builder.RegisterType<OrcamentoMaoDeObraService>().As<IOrcamentoMaoDeObraService>();
        builder.RegisterType<MonitoramentoHistoricoService>().As<IMonitoramentoHistoricoService>();
        builder.RegisterType<LaudoService>().As<ILaudoService>();
        builder.RegisterType<OrcamentoStatusService>().As<IOrcamentoStatusService>();
        builder.RegisterType<LocalEnvioNFFaturamentoVinculadoService>().As<ILocalEnvioNFFaturamentoVinculadoService>();
        builder.RegisterType<LocalEnvioNFFaturamentoService>().As<ILocalEnvioNFFaturamentoService>();
        builder.RegisterType<OrcamentoDeslocamentoService>().As<IOrcamentoDeslocamentoService>();
        builder.RegisterType<OrcamentoDescontoService>().As<IOrcamentoDescontoService>();
        builder.RegisterType<VersaoService>().As<IVersaoService>();
        builder.RegisterType<CargoService>().As<ICargoService>();
        builder.RegisterType<PlantaoTecnicoService>().As<IPlantaoTecnicoService>();
        builder.RegisterType<PlantaoTecnicoRegiaoService>().As<IPlantaoTecnicoRegiaoService>();
        builder.RegisterType<PlantaoTecnicoClienteService>().As<IPlantaoTecnicoClienteService>();
        builder.RegisterType<FormaPagamentoService>().As<IFormaPagamentoService>();
        builder.RegisterType<MoedaService>().As<IMoedaService>();
        builder.RegisterType<PecaListaService>().As<IPecaListaService>();
        builder.RegisterType<TipoFreteService>().As<ITipoFreteService>();
        builder.RegisterType<ClienteBancadaService>().As<IClienteBancadaService>();
        builder.RegisterType<FerramentaTecnicoService>().As<IFerramentaTecnicoService>();
        builder.RegisterType<LiderTecnicoService>().As<ILiderTecnicoService>();
        builder.RegisterType<BancadaListaService>().As<IBancadaListaService>();
        builder.RegisterType<AcaoComponenteService>().As<IAcaoComponenteService>();
        builder.RegisterType<DefeitoComponenteService>().As<IDefeitoComponenteService>();
        builder.RegisterType<EquipamentoModuloService>().As<IEquipamentoModuloService>();
        builder.RegisterType<ClientePecaService>().As<IClientePecaService>();
        builder.RegisterType<ClientePecaGenericaService>().As<IClientePecaGenericaService>();
        builder.RegisterType<CheckinCheckoutService>().As<ICheckinCheckoutService>();            
        builder.RegisterType<SatTaskService>().As<ISatTaskService>();
        builder.RegisterType<SmsService>().As<ISmsService>();
        builder.RegisterType<TicketService>().As<ITicketService>();
        builder.RegisterType<TicketModuloService>().As<ITicketModuloService>();
        builder.RegisterType<TicketStatusService>().As<ITicketStatusService>();
        builder.RegisterType<TicketAtendimentoService>().As<ITicketAtendimentoService>();
        builder.RegisterType<TicketPrioridadeService>().As<ITicketPrioridadeService>();
        builder.RegisterType<TicketClassificacaoService>().As<ITicketClassificacaoService>();
        builder.RegisterType<IntegracaoFinanceiroService>().As<IIntegracaoFinanceiroService>();
        builder.RegisterType<PosVendaService>().As<IPosVendaService>();
        builder.RegisterType<OrcFormaPagamentoService>().As<IOrcFormaPagamentoService>();
        builder.RegisterType<OrcDadosBancariosService>().As<IOrcDadosBancariosService>();
        builder.RegisterType<OrcamentoFaturamentoService>().As<IOrcamentoFaturamentoService>();
        builder.RegisterType<ConferenciaService>().As<IConferenciaService>();
        builder.RegisterType<ConferenciaParticipanteService>().As<IConferenciaParticipanteService>();
        builder.RegisterType<MensagemTecnicoService>().As<IMensagemTecnicoService>();
        builder.RegisterType<IntegracaoBanrisulService>().As<IIntegracaoBanrisulService>();
        builder.RegisterType<ArquivoBanrisulService>().As<IArquivoBanrisulService>();
        builder.RegisterType<OrdemServicoSTNService>().As<IOrdemServicoSTNService>();
        builder.RegisterType<ProtocoloSTNService>().As<IProtocoloSTNService>();
        builder.RegisterType<StatusServicoSTNService>().As<IStatusServicoSTNService>();
        builder.RegisterType<NLogService>().As<INLogService>();
        builder.RegisterType<StatusServicoSTNService>().As<IStatusServicoSTNService>();
        builder.RegisterType<IntegracaoService>().As<IIntegracaoService>();
        builder.RegisterType<LaboratorioService>().As<ILaboratorioService>();
        #endregion

        #region Utils Services
        builder.RegisterType<TokenService>().As<ITokenService>();
        builder.RegisterType<IISLogService>().As<IIISLogService>();
        #endregion
    }
}

