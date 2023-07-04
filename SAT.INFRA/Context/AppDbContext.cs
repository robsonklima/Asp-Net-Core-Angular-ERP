using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Mapping;
using SAT.MODELS.Entities;
using SAT.MODELS.Views;

namespace SAT.INFRA.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<OrdemServico> OrdemServico { get; set; }
        public DbSet<Chamado> Chamado { get; set; }
        public DbSet<OperadoraTelefonia> OperadoraTelefonia { get; set; }
        public DbSet<DefeitoPOS> DefeitoPOS { get; set; }
        public DbSet<ChamadoDadosAdicionais> ChamadoDadosAdicionais { get; set; }
        public DbSet<RelatorioAtendimento> RelatorioAtendimento { get; set; }
        public DbSet<RelatorioAtendimentoDetalhe> RelatorioAtendimentoDetalhe { get; set; }
        public DbSet<RelatorioAtendimentoDetalhePeca> RelatorioAtendimentoDetalhePeca { get; set; }
        public DbSet<StatusServico> StatusServico { get; set; }
        public DbSet<StatusServicoSTN> StatusServicoSTN { get; set; }
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
        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<AuditoriaStatus> AuditoriaStatus { get; set; }
        public DbSet<AuditoriaFoto> AuditoriaFoto { get; set; }
        public DbSet<AcordoNivelServico> AcordoNivelServico { get; set; }
        public DbSet<Navegacao> Navegacao { get; set; }
        public DbSet<NavegacaoConfiguracao> NavegacaoConfiguracao { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<UnidadeFederativa> UnidadeFederativa { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Peca> Peca { get; set; }
        public DbSet<PecaStatus> PecaStatus { get; set; }
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
        public DbSet<ContratoServico> ContratoServico { get; set; }
        public DbSet<AgendaTecnico> AgendaTecnico { get; set; }
        public DbSet<DispBBCalcEquipamentoContrato> DispBBCalcEquipamentoContrato { get; set; }
        public DbSet<DispBBCriticidade> DispBBCriticidade { get; set; }
        public DbSet<DispBBRegiaoFilial> DispBBRegiaoFilial { get; set; }
        public DbSet<DispBBPercRegiao> DispBBPercRegiao { get; set; }
        public DbSet<ViewTecnicoDeslocamento> ViewTecnicoDeslocamento { get; set; }
        public DbSet<DispBBDesvio> DispBBDesvio { get; set; }
        public DbSet<PontoMovel> PontoMovel { get; set; }
        public DbSet<PontoMovelTipoHorario> PontoMovelTipoHorario { get; set; }
        public DbSet<PontoPeriodo> PontoPeriodo { get; set; }
        public DbSet<ORCheckList> ORCheckList { get; set; }
        public DbSet<ORCheckListItem> ORCheckListItem { get; set; }
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
        public DbSet<PlantaoTecnicoRegiao> PlantaoTecnicoRegiao { get; set; }
        public DbSet<PlantaoTecnicoCliente> PlantaoTecnicoCliente { get; set; }
        public DbSet<DespesaAdiantamentoPeriodo> DespesaAdiantamentoPeriodo { get; set; }
        public DbSet<DespesaAdiantamento> DespesaAdiantamento { get; set; }
        public DbSet<DespesaConfiguracaoCombustivel> DespesaConfiguracaoCombustivel { get; set; }
        public DbSet<DespesaItem> DespesaItem { get; set; }
        public DbSet<DespesaPeriodo> DespesaPeriodo { get; set; }
        public DbSet<DespesaProtocolo> DespesaProtocolo { get; set; }
        public DbSet<DespesaPeriodoTecnico> DespesaPeriodoTecnico { get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<DespesaTipo> DespesaTipo { get; set; }
        public DbSet<DespesaAdiantamentoSolicitacao> DespesaAdiantamentoSolicitacao { get; set; }
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
        public DbSet<InstalacaoNFVenda> InstalacaoNFVenda { get; set; }
        public DbSet<InstalacaoPleito> InstalacaoPleito { get; set; }        
        public DbSet<InstalacaoPleitoInstal> InstalacaoPleitoInstal { get; set; }   
        public DbSet<InstalacaoTipoPleito> InstalacaoTipoPleito { get; set; }        
        public DbSet<InstalacaoAnexo> InstalacaoAnexo { get; set; }
        public DbSet<InstalacaoPagto> InstalacaoPagto { get; set; }        
        public DbSet<InstalacaoPagtoInstal> InstalacaoPagtoInstal { get; set; }                
        public DbSet<InstalacaoMotivoMulta> InstalacaoMotivoMulta { get; set; }                
        public DbSet<InstalacaoTipoParcela> InstalacaoTipoParcela { get; set; }                
        public DbSet<Laudo> Laudo { get; set; }
        public DbSet<LaudoStatus> LaudoStatus { get; set; }
        public DbSet<LaudoSituacao> LaudoSituacao { get; set; }
        public DbSet<FiltroUsuario> FiltroUsuario { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<UsuarioDispositivo> UsuarioDispositivo { get; set; }
        public DbSet<OrdemServicoHistorico> OrdemServicoHistorico { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<OrcamentoFaturamento> OrcamentoFaturamento { get; set; }
        public DbSet<OrcamentoMaterial> OrcamentoMaterial { get; set; }
        public DbSet<OrcamentoMotivo> OrcamentoMotivo { get; set; }
        public DbSet<OrcamentoDesconto> OrcamentoDesconto { get; set; }
        public DbSet<OrcamentoMaoDeObra> OrcamentoMaoDeObra { get; set; }
        public DbSet<OrcamentoOutroServico> OrcamentoOutroServico { get; set; }
        public DbSet<Monitoramento> Monitoramento { get; set; }
        public DbSet<MonitoramentoHistorico> MonitoramentoHistorico { get; set; }
        public DbSet<OrcamentoStatus> OrcamentoStatus { get; set; }
        public DbSet<OrcamentoDeslocamento> OrcamentoDeslocamento { get; set; }
        public DbSet<RecuperaSenha> RecuperaSenha { get; set; }
        public DbSet<Intencao> Intencao { get; set; }
        public DbSet<LocalEnvioNFFaturamento> LocalEnvioNFFaturamento { get; set; }
        public DbSet<LocalEnvioNFFaturamentoVinculado> LocalEnvioNFFaturamentoVinculado { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<UsuarioSeguranca> UsuarioSeguranca { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }
        public DbSet<Moeda> Moeda { get; set; }
        public DbSet<PecaLista> PecaLista { get; set; }
        public DbSet<TipoFrete> TipoFrete { get; set; }
        public DbSet<AcordoNivelServicoLegado> AcordoNivelServicoLegado { get; set; }
        public DbSet<ClienteBancada> ClienteBancada { get; set; }
        public DbSet<BancadaLista> BancadaLista { get; set; }
        public DbSet<FerramentaTecnico> FerramentaTecnico { get; set; }
        public DbSet<LiderTecnico> LiderTecnico { get; set; }
        public DbSet<AcaoComponente> AcaoComponente { get; set; }
        public DbSet<DefeitoComponente> DefeitoComponente { get; set; }
        public DbSet<EquipamentoModulo> EquipamentoModulo { get; set; }
        public DbSet<ClientePeca> ClientePeca { get; set; }
        public DbSet<ClientePecaGenerica> ClientePecaGenerica { get; set; }
        public DbSet<ViewOrcamentoLista> ViewOrcamentoLista{ get; set; }
        public DbSet<ViewAgendaTecnicoEvento> ViewAgendaTecnicoEvento { get; set; }
        public DbSet<ViewTecnicoTempoAtendimento> ViewTecnicoTempoAtendimento { get; set; }
		public DbSet<ImportacaoConfiguracao> ImportacaoConfiguracao { get; set; }
        public DbSet<ImportacaoTipo> ImportacaoTipo { get; set; }
        public DbSet<CheckinCheckout> CheckinCheckout { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketModulo> TicketModulo { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<TicketAtendimento> TicketAtendimento { get; set; }
        public DbSet<TicketPrioridade> TicketPrioridade { get; set; }
        public DbSet<TicketClassificacao> TicketClassificacao { get; set; }
        public DbSet<DispBBBloqueioOS> DispBBBloqueioOS { get; set; }
        public DbSet<IntegracaoCobra> IntegracaoCobra { get; set; }
        public DbSet<SatTask> SatTask { get; set; }
        public DbSet<OrcDadosBancarios> OrcDadosBancarios { get; set; }
        public DbSet<OrcFormaPagamento> OrcFormaPagamento { get; set; }
        public DbSet<PosVenda> PosVenda { get; set; }
        public DbSet<OrcIntegracaoFinanceiro> OrcIntegracaoFinanceiro { get; set; }
        public DbSet<AuditoriaVeiculo> AuditoriaVeiculo { get; set; }
        public DbSet<AuditoriaVeiculoAcessorio> AuditoriaVeiculoAcessorio { get; set; }
        public DbSet<AuditoriaVeiculoTanque> AuditoriaVeiculoTanque { get; set; }
        public DbSet<Conferencia> Conferencia { get; set; }
        public DbSet<ConferenciaParticipante> ConferenciaParticipante { get; set; }
        public DbSet<ArquivoBanrisul> ArquivoBanrisul { get; set; }
        public DbSet<ProtocoloSTN> ProtocoloSTN { get; set; }
        public DbSet<Integracao> Integracao { get; set; }
        public DbSet<ORItem> ORItem { get; set; }
        public DbSet<ORStatus> ORStatus { get; set; }
        public DbSet<ORTipo> ORTipo { get; set; }
        public DbSet<BancadaLaboratorio> BancadaLaboratorio { get; set; }
        public DbSet<OR> OR { get; set; }
        public DbSet<UsuarioLogin> UsuarioLogin { get; set; }
        public DbSet<ORDestino> ORDestino { get; set; }
        public DbSet<ItemXORCheckList> ItemXORCheckList { get; set; }
        public DbSet<TicketLogTransacao> TicketLogTransacao { get; set; }
        public DbSet<ViewDespesaImpressaoItem> ViewDespesaImpressaoItem { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosSPACliente> ViewDashboardIndicadoresDetalhadosSPACliente { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosSPATecnico> ViewDashboardIndicadoresDetalhadosSPATecnico { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosSPARegiao> ViewDashboardIndicadoresDetalhadosSPARegiao { get; set; } 
        public DbSet<ViewDashboardIndicadoresDetalhadosProdutividade> ViewDashboardIndicadoresDetalhadosProdutividade { get; set; } 
        public DbSet<ViewDashboardIndicadoresFiliais> ViewDashboardIndicadoresFiliais { get; set; }
        public DbSet<ViewDashboardChamadosMaisAntigosCorretivas> ViewDashboardChamadosMaisAntigosCorretivas { get; set; }
        public DbSet<ViewDashboardChamadosMaisAntigosOrcamentos> ViewDashboardChamadosMaisAntigosOrcamentos { get; set; }
        public DbSet<ViewDashboardDisponibilidadeTecnicos> ViewDashboardDisponibilidadeTecnicos { get; set; }
        public DbSet<ViewDashboardDisponibilidadeTecnicosMediaGlobal> ViewDashboardDisponibilidadeTecnicosMediaGlobal { get; set; }
        public DbSet<ViewDashboardSPA> ViewDashboardSPA { get; set; }
        public DbSet<ViewDashboardSLAClientes> ViewDashboardSLAClientes { get; set; }
        public DbSet<ViewDashboardReincidenciaFiliais> ViewDashboardReincidenciaFiliais { get; set; }
        public DbSet<ViewDashboardReincidenciaQuadrimestreFiliais> ViewDashboardReincidenciaQuadrimestreFiliais { get; set; }
        public DbSet<ViewDashboardReincidenciaClientes> ViewDashboardReincidenciaClientes { get; set; }
        public DbSet<ViewDashboardSPATecnicosMaiorDesempenho> ViewDashboardSPATecnicosMaiorDesempenho { get; set; }
        public DbSet<ViewDashboardSPATecnicosMenorDesempenho> ViewDashboardSPATecnicosMenorDesempenho { get; set; }
        public DbSet<ViewDashboardReincidenciaTecnicosMenosReincidentes> ViewDashboardReincidenciaTecnicosMenosReincidentes { get; set; }
        public DbSet<ViewDashboardReincidenciaTecnicosMaisReincidentes> ViewDashboardReincidenciaTecnicosMaisReincidentes { get; set; }
        public DbSet<ViewDashboardEquipamentosMaisReincidentes> ViewDashboardEquipamentosMaisReincidentes { get; set; }
        public DbSet<ViewDashboardPendenciaFiliais> ViewDashboardPendenciaFiliais { get; set; }
        public DbSet<ViewDashboardPendenciaQuadrimestreFiliais> ViewDashboardPendenciaQuadrimestreFiliais { get; set; }
        public DbSet<ViewDashboardTecnicosMenosPendentes> ViewDashboardTecnicosMenosPendentes { get; set; }
        public DbSet<ViewDashboardTecnicosMaisPendentes> ViewDashboardTecnicosMaisPendentes { get; set; }
        public DbSet<ViewDashboardPendenciaGlobal> ViewDashboardPendenciaGlobal { get; set; }
        public DbSet<ViewDashboardPecasFaltantes> ViewDashboardPecasFaltantes { get; set; }
        public DbSet<ViewDashboardPecasMaisFaltantes> ViewDashboardPecasMaisFaltantes { get; set; }
        public DbSet<ViewDashboardPecasCriticasMaisFaltantes> ViewDashboardPecasCriticasMaisFaltantes { get; set; }
        public DbSet<ViewDashboardPecasCriticaChamadosFaltantes> ViewDashboardPecasCriticaChamadosFaltantes { get; set; }
        public DbSet<ViewDashboardPecasCriticaEstoqueFaltantes> ViewDashboardPecasCriticaEstoqueFaltantes { get; set; }
        public DbSet<ViewDashboardDensidadeEquipamentos> ViewDashboardDensidadeEquipamentos { get; set; }
        public DbSet<ViewDashboardDensidadeTecnicos> ViewDashboardDensidadeTecnicos { get; set; }
        public DbSet<ViewDashboardDisponibilidadeBBTSFiliais> ViewDashboardDisponibilidadeBBTSFiliais { get; set; }
        public DbSet<ViewMediaDespesasAdiantamento> ViewMediaDespesasAdiantamento { get; set; }
        public DbSet<ViewDashboardDisponibilidadeBBTSMapaRegioes> ViewDashboardDisponibilidadeBBTSMapaRegioes { get; set; }
        public DbSet<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> ViewDashboardDisponibilidadeBBTSMultasDisponibilidade { get; set; }
        public DbSet<ViewDashboardDisponibilidadeBBTSMultasRegioes> ViewDashboardDisponibilidadeBBTSMultasRegioes { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosSLATecnico> ViewDashboardIndicadoresDetalhadosSLATecnico { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosSLACliente> ViewDashboardIndicadoresDetalhadosSLACliente { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosSLARegiao> ViewDashboardIndicadoresDetalhadosSLARegiao { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosPendenciaTecnico> ViewDashboardIndicadoresDetalhadosPendenciaTecnico { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosPendenciaCliente> ViewDashboardIndicadoresDetalhadosPendenciaCliente { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosPendenciaRegiao> ViewDashboardIndicadoresDetalhadosPendenciaRegiao { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosReincidenciaTecnico> ViewDashboardIndicadoresDetalhadosReincidenciaTecnico { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosReincidenciaCliente> ViewDashboardIndicadoresDetalhadosReincidenciaCliente { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosReincidenciaRegiao> ViewDashboardIndicadoresDetalhadosReincidenciaRegiao { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosPerformance> ViewDashboardIndicadoresDetalhadosPerformance { get; set; }
        public DbSet<ViewDashboardIndicadoresDetalhadosChamadosAntigos> ViewDashboardIndicadoresDetalhadosChamadosAntigos { get; set; }
        public DbSet<ViewExportacaoChamadosUnificado> ViewExportacaoChamadosUnificado { get; set; }
        public DbSet<ViewExportacaoInstalacao> ViewExportacaoInstalacao { get; set; }        
        public DbSet<ViewIntegracaoFinanceiroOrcamento> ViewIntegracaoFinanceiroOrcamento { get; set; }
        public DbSet<ViewIntegracaoFinanceiroOrcamentoItem> ViewIntegracaoFinanceiroOrcamentoItem { get; set; }
        public DbSet<MensagemTecnico> MensagemTecnico { get; set; }
        public DbSet<ViewIntegracaoBB> ViewIntegracaoBB { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
        public DbSet<OrdemServicoSTN> OrdemServicoSTN { get; set; }
        public DbSet<OrdemServicoSTNOrigem> OrdemServicoSTNOrigem { get; set; }
        public DbSet<ViewLaboratorioTecnicoBancada> ViewLaboratorioTecnicoBancada { get; set; }
        public DbSet<AuditoriaView> AuditoriaView { get; set; }
        public DbSet<ORTempoReparo> ORTempoReparo { get; set; }
        public DbSet<ORItemInsumo> ORItemInsumo { get; set; }
        public DbSet<ORTransporte> ORTransporte { get; set; }
        public DbSet<ORDefeito> ORDefeito { get; set; }
        public DbSet<ORSolucao> ORSolucao { get; set; }
        public DbSet<ItemDefeito> ItemDefeito { get; set; }
        public DbSet<ItemSolucao> ItemSolucao { get; set; }
        public DbSet<TicketAnexo> TicketAnexo { get; set; }
        public DbSet<TicketBacklogView> TicketBacklogView { get; set; }
        public DbSet<AdiantamentoRDsPendentesView> AdiantamentoRDsPendentesView { get; set; }        
        public DbSet<ProtocoloChamadoSTN> ProtocoloChamadoSTN { get; set; }
        public DbSet<TipoChamadoSTN> TipoChamadoSTN { get; set; }
        public DbSet<Improdutividade> Improdutividade { get; set; }
        public DbSet<CausaImprodutividade> CausaImprodutividade { get; set; }
        public DbSet<CheckListPOS> CheckListPOS { get; set; }
        public DbSet<CheckListPOSItens> CheckListPOSItens { get; set; }
        public DbSet<PecasLaboratorio> PecasLaboratorio { get; set; }
        public DbSet<ViewDashboardLabProdutividadeTecnica> ViewDashboardLabProdutividadeTecnica { get; set; }
        public DbSet<ViewDashboardLabTopItensMaisAntigos> ViewDashboardLabTopItensMaisAntigos { get; set; }
        public DbSet<ViewDashboardLabTopTempoMedioReparo> ViewDashboardLabTopTempoMedioReparo { get; set; }
        public DbSet<ViewDashboardLabIndiceReincidencia> ViewDashboardLabIndiceReincidencia { get; set; }
        public DbSet<ViewDashboardLabRecebidosReparados> ViewDashboardLabRecebidosReparados { get; set; }
        public DbSet<ViewDashboardLabTopFaltantes> ViewDashboardLabTopFaltantes { get; set; }
        public DbSet<OSBancada> OSBancada { get; set; }
        public DbSet<PecaRE5114> PecaRE5114 { get; set; }
        public DbSet<OSBancadaPecas> OSBancadaPecas { get; set; }
        public DbSet<OsBancadaPecasOrcamento> OsBancadaPecasOrcamento { get; set; }
        public DbSet<OrcamentoPecasEspec> OrcamentoPecasEspec { get; set; }
        public DbSet<InstalacaoStatus> InstalacaoStatus { get; set; }
        public DbSet<RelatorioAtendimentoDetalhePecaStatus> RelatorioAtendimentoDetalhePecaStatus { get; set; }
        public DbSet<RelatorioAtendimentoPecaStatus> RelatorioAtendimentoPecaStatus { get; set; }
        public DbSet<InstalacaoView> InstalacaoView { get; set; }
        public DbSet<RelatorioAtendimentoPOS> RelatorioAtendimentoPOS { get; set; }
        public DbSet<MotivoComunicacao> MotivoComunicacao { get; set; }
        public DbSet<MotivoCancelamento> MotivoCancelamento { get; set; }
        public DbSet<RedeBanrisul> RedeBanrisul { get; set; }
        public DbSet<TipoComunicacao> TipoComunicacao { get; set; }
        public DbSet<EquipamentoPOS> EquipamentoPOS { get; set; }
        public DbSet<StatusEquipamentoPOS> StatusEquipamentoPOS { get; set; }
        public DbSet<MRPLogix> MRPLogix { get; set; }        
        public DbSet<MRPLogixEstoque> MRPLogixEstoque { get; set; }
        public DbSet<Setor> Setor { get; set; }
        public DbSet<PerfilSetor> PerfilSetor { get; set; }
        public DbSet<SatTaskTipo> SatTaskTipo { get; set; }
        public DbSet<ANS> ANS { get; set; }
        public DbSet<OSPrazoAtendimento> OSPrazoAtendimento { get; set; }
        public DbSet<SATFeriado> SATFeriado { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sequencia>(new SequenciaMap().Configure);
            modelBuilder.Entity<ContratoEquipamento>(new ContratoEquipamentoMap().Configure);
            modelBuilder.Entity<ContratoSLA>(new ContratoSLAMap().Configure);
            modelBuilder.Entity<ContratoServico>(new ContratoServicoMap().Configure);
            modelBuilder.Entity<AgendaTecnico>(new AgendaTecnicoMap().Configure);
            modelBuilder.Entity<DespesaPeriodoTecnico>(new DespesaPeriodoTecnicoMap().Configure);
            modelBuilder.Entity<TicketLogPedidoCredito>(new TicketLogPedidoCreditoMap().Configure);
            modelBuilder.Entity<TicketLogUsuarioCartaoPlaca>(new TicketLogUsuarioCartaoPlacaMap().Configure);
            modelBuilder.Entity<DespesaCartaoCombustivel>(new DespesaCartaoCombustivelMap().Configure);
            modelBuilder.Entity<Tecnico>(new TecnicoMap().Configure);
            modelBuilder.Entity<DespesaCartaoCombustivelTecnico>(new DespesaCartaoCombustivelTecnicoMap().Configure);
            modelBuilder.Entity<TecnicoConta>(new TecnicoContaMap().Configure);
            modelBuilder.Entity<Instalacao>(new InstalacaoMap().Configure);
            modelBuilder.Entity<InstalacaoNFVenda>(new InstalacaoNFVendaMap().Configure);
            modelBuilder.Entity<Laudo>(new LaudoMap().Configure);
            modelBuilder.Entity<LaudoStatus>(new LaudoStatusMap().Configure);
            modelBuilder.Entity<LaudoSituacao>(new LaudoSituacaoMap().Configure);
            modelBuilder.Entity<DespesaAdiantamentoSolicitacao>(new DespesaAdiantamentoSolicitacaoMap().Configure);
            modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
            modelBuilder.Entity<EquipamentoContrato>(new EquipamentoContratoMap().Configure);
            modelBuilder.Entity<UnidadeFederativa>(new UnidadeFederativaMap().Configure);
            modelBuilder.Entity<DispBBRegiao>(new DispBBRegiaoMap().Configure);
            modelBuilder.Entity<DispBBCalcEquipamentoContrato>(new DispBBCalcEquipamentoContratoMap().Configure);
            modelBuilder.Entity<RelatorioAtendimento>(new RelatorioAtendimentoMap().Configure);
            modelBuilder.Entity<RelatorioAtendimentoDetalhe>(new RelatorioAtendimentoDetalheMap().Configure);
            modelBuilder.Entity<Foto>(new FotoMap().Configure);
            modelBuilder.Entity<UsuarioDispositivo>(new UsuarioDispositivoMap().Configure);
            modelBuilder.Entity<OrdemServico>(new OrdemServicoMap().Configure);
            modelBuilder.Entity<OrdemServicoHistorico>(new OrdemServicoHistoricoMap().Configure);
            modelBuilder.Entity<Orcamento>(new OrcamentoMap().Configure);
            modelBuilder.Entity<OrcamentoMotivo>(new OrcamentoMotivoMap().Configure);
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
            modelBuilder.Entity<Peca>(new PecaMap().Configure);
            modelBuilder.Entity<ClientePeca>(new ClientePecaMap().Configure);
            modelBuilder.Entity<ClientePecaGenerica>(new ClientePecaGenericaMap().Configure);
            modelBuilder.Entity<TecnicoCliente>(new TecnicoClienteMap().Configure);
            modelBuilder.Entity<PontoPeriodoUsuario>(new PontoPeriodoUsuarioMap().Configure);
            modelBuilder.Entity<Intencao>(new IntencaoMap().Configure);
            modelBuilder.Entity<LocalEnvioNFFaturamento>(new LocalEnvioNFFaturamentoMap().Configure);
            modelBuilder.Entity<LocalEnvioNFFaturamentoVinculado>(new LocalEnvioNFFaturamentoVinculadoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresFiliais>(new ViewDashboardIndicadoresFiliaisMap().Configure);
            modelBuilder.Entity<ViewDashboardChamadosMaisAntigosCorretivas>(new ViewDashboardChamadosMaisAntigosCorretivasMap().Configure);
            modelBuilder.Entity<ViewDashboardChamadosMaisAntigosOrcamentos>(new ViewDashboardChamadosMaisAntigosOrcamentosMap().Configure);
            modelBuilder.Entity<ViewDashboardDisponibilidadeTecnicos>(new ViewDashboardDisponibilidadeTecnicosMap().Configure);
            modelBuilder.Entity<ViewDashboardDisponibilidadeTecnicosMediaGlobal>(new ViewDashboardDisponibilidadeTecnicosMediaGlobalMap().Configure);
            modelBuilder.Entity<ViewDashboardSPA>(new ViewDashboardSPAMap().Configure);
            modelBuilder.Entity<ViewDashboardSLAClientes>(new ViewDashboardSLAClientesMap().Configure);
            modelBuilder.Entity<ViewDashboardReincidenciaFiliais>(new ViewDashboardReincidenciaFiliaisMap().Configure);
            modelBuilder.Entity<ViewDashboardReincidenciaQuadrimestreFiliais>(new ViewDashboardReincidenciaQuadrimestreFiliaisMap().Configure);
            modelBuilder.Entity<ViewDashboardReincidenciaClientes>(new ViewDashboardReincidenciaClientesMap().Configure);
            modelBuilder.Entity<ViewDashboardSPATecnicosMenorDesempenho>(new ViewDashboardSPATecnicosMenorDesempenhoMap().Configure);
            modelBuilder.Entity<ViewDashboardSPATecnicosMaiorDesempenho>(new ViewDashboardSPATecnicosMaiorDesempenhoMap().Configure);
            modelBuilder.Entity<ViewDashboardReincidenciaTecnicosMenosReincidentes>(new ViewDashboardReincidenciaTecnicosMenosReincidentesMap().Configure);
            modelBuilder.Entity<ViewDashboardReincidenciaTecnicosMaisReincidentes>(new ViewDashboardReincidenciaTecnicosMaisReincidentesMap().Configure);
            modelBuilder.Entity<ViewDashboardEquipamentosMaisReincidentes>(new ViewDashboardEquipamentosMaisReincidentesMap().Configure);
            modelBuilder.Entity<ViewDashboardPendenciaFiliais>(new ViewDashboardPendenciaFiliaisMap().Configure);
            modelBuilder.Entity<ViewDashboardPendenciaQuadrimestreFiliais>(new ViewDashboardPendenciaQuadrimestreFiliaisMap().Configure);
            modelBuilder.Entity<ViewDashboardTecnicosMenosPendentes>(new ViewDashboardTecnicosMenosPendentesMap().Configure);
            modelBuilder.Entity<ViewDashboardTecnicosMaisPendentes>(new ViewDashboardTecnicosMaisPendentesMap().Configure);
            modelBuilder.Entity<ViewDashboardPendenciaGlobal>(new ViewDashboardPendenciaGlobalMap().Configure);
            modelBuilder.Entity<ViewDashboardPecasFaltantes>(new ViewDashboardPecasFaltantesMap().Configure);
            modelBuilder.Entity<ViewDashboardPecasMaisFaltantes>(new ViewDashboardPecasMaisFaltantesMap().Configure);
            modelBuilder.Entity<ViewDashboardPecasCriticasMaisFaltantes>(new ViewDashboardPecasCriticasMaisFaltantesMap().Configure);
            modelBuilder.Entity<ViewDashboardPecasCriticaChamadosFaltantes>(new ViewDashboardPecasCriticaChamadosFaltantesMap().Configure);
            modelBuilder.Entity<ViewDashboardPecasCriticaEstoqueFaltantes>(new ViewDashboardPecasCriticaEstoqueFaltantesMap().Configure);
            modelBuilder.Entity<ViewMediaDespesasAdiantamento>(new ViewMediaDespesasAdiantamentoMap().Configure);
            modelBuilder.Entity<ViewDashboardDensidadeEquipamentos>(new ViewDashboardDensidadeEquipamentosMap().Configure);
            modelBuilder.Entity<ViewDashboardDensidadeTecnicos>(new ViewDashboardDensidadeTecnicosMap().Configure);
            modelBuilder.Entity<ViewDashboardDisponibilidadeBBTSFiliais>(new ViewDashboardDisponibilidadeBBTSFiliaisMap().Configure);
            modelBuilder.Entity<ViewDashboardDisponibilidadeBBTSMapaRegioes>(new ViewDashboardDisponibilidadeBBTSMapaRegioesMap().Configure);
            modelBuilder.Entity<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade>(new ViewDashboardDisponibilidadeBBTSMultasDisponibilidadeMap().Configure);
            modelBuilder.Entity<ViewDashboardDisponibilidadeBBTSMultasRegioes>(new ViewDashboardDisponibilidadeBBTSMultasRegioesMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosSLACliente>(new ViewDashboardIndicadoresDetalhadosSLAClienteMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosSLARegiao>(new ViewDashboardIndicadoresDetalhadosSLARegiaoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosSLATecnico>(new ViewDashboardIndicadoresDetalhadosSLATecnicoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosPendenciaTecnico>(new ViewDashboardIndicadoresDetalhadosPendenciaTecnicoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosPendenciaRegiao>(new ViewDashboardIndicadoresDetalhadosPendenciaRegiaoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosPendenciaCliente>(new ViewDashboardIndicadoresDetalhadosPendenciaClienteMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosReincidenciaCliente>(new ViewDashboardIndicadoresDetalhadosReincidenciaClienteMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosReincidenciaTecnico>(new ViewDashboardIndicadoresDetalhadosReincidenciaTecnicoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosReincidenciaRegiao>(new ViewDashboardIndicadoresDetalhadosReincidenciaRegiaoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosPerformance>(new ViewDashboardIndicadoresDetalhadosPerformanceMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosChamadosAntigos>(new ViewDashboardIndicadoresDetalhadosChamadosAntigosMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosSPACliente>(new ViewDashboardIndicadoresDetalhadosSPAClienteMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosSPARegiao>(new ViewDashboardIndicadoresDetalhadosSPARegiaoMap().Configure);
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosSPATecnico>(new ViewDashboardIndicadoresDetalhadosSPATecnicoMap().Configure);    
            modelBuilder.Entity<ViewDashboardIndicadoresDetalhadosProdutividade>(new ViewDashboardIndicadoresDetalhadosProdutividadeMap().Configure);                            
            modelBuilder.Entity<ViewExportacaoChamadosUnificado>(new ViewExportacaoChamadosUnificadoMap().Configure);
            modelBuilder.Entity<ViewExportacaoInstalacao>(new ViewExportacaoInstalacaoMap().Configure);
            modelBuilder.Entity<ViewIntegracaoFinanceiroOrcamento>(new ViewIntegracaoFinanceiroOrcamentoMap().Configure);
            modelBuilder.Entity<ViewIntegracaoFinanceiroOrcamentoItem>(new ViewIntegracaoFinanceiroOrcamentoItemMap().Configure);
            modelBuilder.Entity<ViewOrcamentoLista>(new ViewOrcamentoListaMap().Configure);
            modelBuilder.Entity<PlantaoTecnico>(new PlantaoTecnicoMap().Configure);
            modelBuilder.Entity<PlantaoTecnicoRegiao>(new PlantaoTecnicoRegiaoMap().Configure);
            modelBuilder.Entity<PlantaoTecnicoCliente>(new PlantaoTecnicoClienteMap().Configure);
            modelBuilder.Entity<TecnicoVeiculo>(new TecnicoVeiculoMap().Configure);
            modelBuilder.Entity<VeiculoCombustivel>(new VeiculoCombustivelMap().Configure);
            modelBuilder.Entity<FormaPagamento>(new FormaPagamentoMap().Configure);
            modelBuilder.Entity<Moeda>(new MoedaMap().Configure);
            modelBuilder.Entity<PecaLista>(new PecaListaMap().Configure);
            modelBuilder.Entity<TipoFrete>(new TipoFreteMap().Configure);
            modelBuilder.Entity<AcordoNivelServico>(new AcordoNivelServicoMap().Configure);
            modelBuilder.Entity<AcordoNivelServicoLegado>(new AcordoNivelServicoLegadoMap().Configure);
            modelBuilder.Entity<Equipamento>(new EquipamentoMap().Configure);
            modelBuilder.Entity<TipoEquipamento>(new TipoEquipamentoMap().Configure);
            modelBuilder.Entity<ClienteBancada>(new ClienteBancadaMap().Configure);
            modelBuilder.Entity<FerramentaTecnico>(new FerramentaTecnicoMap().Configure);
            modelBuilder.Entity<Feriado>(new FeriadoMap().Configure);
            modelBuilder.Entity<LiderTecnico>(new LiderTecnicoMap().Configure);
            modelBuilder.Entity<BancadaLista>(new BancadaListaMap().Configure);
            modelBuilder.Entity<AcaoComponente>(new AcaoComponenteMap().Configure);
            modelBuilder.Entity<Acao>(new AcaoMap().Configure);
            modelBuilder.Entity<Causa>(new CausaMap().Configure);
            modelBuilder.Entity<DefeitoComponente>(new DefeitoComponenteMap().Configure);
            modelBuilder.Entity<EquipamentoModulo>(new EquipamentoModuloMap().Configure);
            modelBuilder.Entity<ImportacaoConfiguracao>(new ImportacaoConfiguracaoMap().Configure);
            modelBuilder.Entity<ImportacaoTipo>(new ImportacaoTipoMap().Configure);
            modelBuilder.Entity<ViewAgendaTecnicoEvento>(new ViewAgendaTecnicoEventoMap().Configure);
            modelBuilder.Entity<ViewTecnicoTempoAtendimento>(new ViewTecnicoTempoAtendimentoMap().Configure);
            modelBuilder.Entity<Chamado>(new ChamadoMap().Configure);
            modelBuilder.Entity<Ticket>(new TicketMap().Configure);
            modelBuilder.Entity<DispBBBloqueioOS>(new DispBBBloqueioOSMap().Configure);
            modelBuilder.Entity<IntegracaoCobra>(new IntegracaoCobraMap().Configure);
            modelBuilder.Entity<SatTask>(new SatTaskMap().Configure);
            modelBuilder.Entity<OrcFormaPagamento>(new OrcFormaPagamentoMap().Configure);
            modelBuilder.Entity<OrcDadosBancarios>(new OrcDadosBancariosMap().Configure);
            modelBuilder.Entity<PosVenda>(new PosVendaMap().Configure);
            modelBuilder.Entity<OrcIntegracaoFinanceiro>(new OrcIntegracaoFinanceiroMap().Configure);
            modelBuilder.Entity<ViewDespesaImpressaoItem>(new ViewDespesaImpressaoItemMap().Configure);
            modelBuilder.Entity<AuditoriaVeiculo>(new AuditoriaVeiculoMap().Configure);
            modelBuilder.Entity<AuditoriaVeiculoAcessorio>(new AuditoriaVeiculoAcessorioMap().Configure);
            modelBuilder.Entity<AuditoriaVeiculoTanque>(new AuditoriaVeiculoTanqueMap().Configure);
            modelBuilder.Entity<AuditoriaStatus>(new AuditoriaStatusMap().Configure);
            modelBuilder.Entity<AuditoriaFoto>(new AuditoriaFotoMap().Configure);
            modelBuilder.Entity<Auditoria>(new AuditoriaMap().Configure);
            modelBuilder.Entity<DespesaConfiguracaoCombustivel>(new DespesaConfiguracaoCombustivelMap().Configure);
            modelBuilder.Entity<OrcamentoFaturamento>(new OrcamentoFaturamentoMap().Configure);
            modelBuilder.Entity<Conferencia>(new ConferenciaMap().Configure);
            modelBuilder.Entity<ConferenciaParticipante>(new ConferenciaParticipanteMap().Configure);
            modelBuilder.Entity<MensagemTecnico>(new MensagemTecnicoMap().Configure);
            modelBuilder.Entity<Cidade>(new CidadeMap().Configure);   
            modelBuilder.Entity<ArquivoBanrisul>(new ArquivoBanrisulMap().Configure);
            modelBuilder.Entity<OrdemServicoSTN>(new OrdemServicoSTNMap().Configure);
            modelBuilder.Entity<StatusServicoSTN>(new StatusServicoSTNMap().Configure);
            modelBuilder.Entity<OrdemServicoSTNOrigem>(new OrdemServicoSTNOrigemMap().Configure);
            modelBuilder.Entity<ProtocoloSTN>(new ProtocoloSTNMap().Configure);
            modelBuilder.Entity<Despesa>(new DespesaMap().Configure);
            modelBuilder.Entity<Integracao>(new IntegracaoMap().Configure);
            modelBuilder.Entity<ViewLaboratorioTecnicoBancada>(new ViewLaboratorioTecnicoBancadaMap().Configure);
            modelBuilder.Entity<ORItem>(new ORItemMap().Configure);
            modelBuilder.Entity<ORStatus>(new ORStatusMap().Configure);
            modelBuilder.Entity<ORTipo>(new ORTipoMap().Configure);
            modelBuilder.Entity<BancadaLaboratorio>(new BancadaLaboratorioMap().Configure);
            modelBuilder.Entity<OR>(new ORMap().Configure);
            modelBuilder.Entity<Mensagem>(new MensagemMap().Configure);
            modelBuilder.Entity<UsuarioLogin>(new UsuarioLoginMap().Configure);
            modelBuilder.Entity<ORDestino>(new ORDestinoMap().Configure);
            modelBuilder.Entity<ORCheckList>(new ORCheckListMap().Configure);
            modelBuilder.Entity<ORCheckListItem>(new ORCheckListItemMap().Configure);
            modelBuilder.Entity<TicketLogTransacao>(new TicketLogTransacaoMap().Configure);
            modelBuilder.Entity<AuditoriaView>(new AuditoriaViewMap().Configure);
            modelBuilder.Entity<ORTempoReparo>(new ORTempoReparoMap().Configure);
            modelBuilder.Entity<ORItemInsumo>(new ORItemInsumoMap().Configure);
            modelBuilder.Entity<ORTransporte>(new ORTransporteMap().Configure);
            modelBuilder.Entity<ORDefeito>(new ORDefeitoMap().Configure);
            modelBuilder.Entity<ORSolucao>(new ORSolucaoMap().Configure);
            modelBuilder.Entity<TicketAtendimento>(new TicketAtendimentoMap().Configure);
            modelBuilder.Entity<ItemXORCheckList>(new ItemXORCheckListMap().Configure);
            modelBuilder.Entity<ItemDefeito>(new ItemDefeitoMap().Configure);
            modelBuilder.Entity<ItemSolucao>(new ItemSolucaoMap().Configure);
            modelBuilder.Entity<TicketAnexo>(new TicketAnexoMap().Configure);
            modelBuilder.Entity<TicketBacklogView>(new TicketBacklogViewMap().Configure);
            modelBuilder.Entity<InstalacaoPleito>(new InstalacaoPleitoMap().Configure);
            modelBuilder.Entity<InstalacaoPleitoInstal>(new InstalacaoPleitoInstalMap().Configure);
            modelBuilder.Entity<InstalacaoTipoPleito>(new InstalacaoTipoPleitoMap().Configure);
            modelBuilder.Entity<InstalacaoPagto>(new InstalacaoPagtoMap().Configure);
            modelBuilder.Entity<InstalacaoPagtoInstal>(new InstalacaoPagtoInstalMap().Configure);
            modelBuilder.Entity<InstalacaoMotivoMulta>(new InstalacaoMotivoMultaMap().Configure);
            modelBuilder.Entity<InstalacaoTipoParcela>(new InstalacaoTipoParcelaMap().Configure);           
            modelBuilder.Entity<ProtocoloChamadoSTN>(new ProtocoloChamadoSTNMap().Configure);
            modelBuilder.Entity<TipoChamadoSTN>(new TipoChamadoSTNMap().Configure);
            modelBuilder.Entity<Improdutividade>(new ImprodutividadeMap().Configure);
            modelBuilder.Entity<CausaImprodutividade>(new CausaImprodutividadeMap().Configure);
            modelBuilder.Entity<CheckListPOS>(new CheckListPOSMap().Configure);
            modelBuilder.Entity<CheckListPOSItens>(new CheckListPOSItensMap().Configure);
            modelBuilder.Entity<PontoUsuario>(new PontoUsuarioMap().Configure);
            modelBuilder.Entity<PecasLaboratorio>(new PecasLaboratorioMap().Configure);
            modelBuilder.Entity<ViewTecnicoDeslocamento>(new ViewTecnicoDeslocamentoMap().Configure);
            modelBuilder.Entity<OSBancada>(new OSBancadaMap().Configure);
            modelBuilder.Entity<PecaRE5114>(new PecaRE5114Map().Configure);
            modelBuilder.Entity<ViewDashboardLabRecebidosReparados>(new ViewDashboardLabRecebidosReparadosMap().Configure);
            modelBuilder.Entity<ViewDashboardLabTopFaltantes>(new ViewDashboardLabTopFaltantesMap().Configure);
            modelBuilder.Entity<ViewDashboardLabProdutividadeTecnica>(new ViewDashboardLabProdutividadeTecnicaMap().Configure);
            modelBuilder.Entity<ViewDashboardLabTopItensMaisAntigos>(new ViewDashboardLabTopItensMaisAntigosMap().Configure);
            modelBuilder.Entity<ViewDashboardLabIndiceReincidencia>(new ViewDashboardLabIndiceReincidenciaMap().Configure);
            modelBuilder.Entity<ViewDashboardLabTopTempoMedioReparo>(new ViewDashboardLabTopTempoMedioReparoMap().Configure);
            modelBuilder.Entity<OSBancadaPecas>(new OSBancadaPecasMap().Configure);
            modelBuilder.Entity<InstalacaoAnexo>(new InstalacaoAnexoMap().Configure);
            modelBuilder.Entity<OsBancadaPecasOrcamento>(new OsBancadaPecasOrcamentoMap().Configure);
            modelBuilder.Entity<OrcamentoPecasEspec>(new OrcamentoPecasEspecMap().Configure);
            modelBuilder.Entity<InstalacaoStatus>(new InstalacaoStatusMap().Configure);
            modelBuilder.Entity<RelatorioAtendimentoDetalhePecaStatus>(new RelatorioAtendimentoDetalhePecaStatusMap().Configure);
            modelBuilder.Entity<RelatorioAtendimentoPecaStatus>(new RelatorioAtendimentoPecaStatusMap().Configure);
            modelBuilder.Entity<AdiantamentoRDsPendentesView>(new AdiantamentoRDsPendentesViewMap().Configure);
            modelBuilder.Entity<InstalacaoView>(new InstalacaoViewMap().Configure);
            modelBuilder.Entity<LocalAtendimento>(new LocalAtendimentoMap().Configure);
            modelBuilder.Entity<RelatorioAtendimentoPOS>(new RelatorioAtendimentoPOSMap().Configure);
            modelBuilder.Entity<MotivoComunicacao>(new MotivoComunicacaoMap().Configure);
            modelBuilder.Entity<MotivoCancelamento>(new MotivoCancelamentoMap().Configure);
            modelBuilder.Entity<RedeBanrisul>(new RedeBanrisulMap().Configure);
            modelBuilder.Entity<TipoComunicacao>(new TipoComunicacaoMap().Configure);
            modelBuilder.Entity<EquipamentoPOS>(new EquipamentoPOSMap().Configure);
            modelBuilder.Entity<StatusEquipamentoPOS>(new StatusEquipamentoPOSMap().Configure);
            modelBuilder.Entity<MRPLogix>(new MRPLogixMap().Configure);
            modelBuilder.Entity<MRPLogixEstoque>(new MRPLogixEstoqueMap().Configure);
            modelBuilder.Entity<Setor>(new SetorMap().Configure);
            modelBuilder.Entity<PerfilSetor>(new PerfilSetorMap().Configure);
            modelBuilder.Entity<SatTaskTipo>(new SatTaskTipoMap().Configure);
            modelBuilder.Entity<ViewIntegracaoBB>(new ViewIntegracaoBBMap().Configure);
            modelBuilder.Entity<ANS>(new ANSMap().Configure);
            modelBuilder.Entity<OSPrazoAtendimento>(new OSPrazoAtendimentoMap().Configure);
            modelBuilder.Entity<SATFeriado>(new SATFeriadoMap().Configure);

            modelBuilder.Entity<RegiaoAutorizada>()
                            .HasKey(ra => new { ra.CodFilial, ra.CodRegiao, ra.CodAutorizada });

            modelBuilder.Entity<NavegacaoConfiguracao>()
                        .HasOne<Navegacao>(nc => nc.Navegacao)
                        .WithMany(nc => nc.NavegacoesConfiguracao);

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }
    }
}