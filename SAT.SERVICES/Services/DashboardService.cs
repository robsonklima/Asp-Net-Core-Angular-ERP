using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            this._dashboardRepository = dashboardRepository;
        }

        public ViewDadosDashboard ObterViewPorParametros(DashboardParameters parameters)
        {
            ViewDadosDashboard viewDashboard = new();

            switch (parameters.DashboardViewEnum)
            {
                case DashboardViewEnum.INDICADORES_FILIAL:
                    viewDashboard.ViewDashboardIndicadoresFiliais = this._dashboardRepository.ObterDadosIndicadorFiliais();
                    break;
                case DashboardViewEnum.CHAMADOS_ANTIGOS_CORRETIVAS:
                    viewDashboard.ViewDashboardChamadosMaisAntigosCorretivas = this._dashboardRepository.ObterChamadosMaisAntigosCorretivas();
                    break;
                case DashboardViewEnum.CHAMADOS_ANTIGOS_ORCAMENTOS:
                    viewDashboard.ViewDashboardChamadosMaisAntigosOrcamentos = this._dashboardRepository.ObterChamadosMaisAntigosOrcamentos();
                    break;
                case DashboardViewEnum.BBTS_FILIAIS:
                    viewDashboard.ViewDashboardDisponibilidadeBBTSFiliais = this._dashboardRepository.ObterIndicadorDisponibilidadeBBTSFiliais();
                    break;
                case DashboardViewEnum.BBTS_MAPA_REGIOES:
                    viewDashboard.ViewDashboardDisponibilidadeBBTSMapaRegioes = this._dashboardRepository.ObterIndicadorDisponibilidadeBBTSMapaRegioes();
                    break;
                case DashboardViewEnum.BBTS_MULTA_REGIOES:
                    viewDashboard.ViewDashboardDisponibilidadeBBTSMultasRegioes = this._dashboardRepository.ObterIndicadorDisponibilidadeBBTSMultasRegioes();
                    break;
                case DashboardViewEnum.BBTS_MULTA_DISPONIBILIDADE:
                    viewDashboard.ViewDashboardDisponibilidadeBBTSMultasDisponibilidade = this._dashboardRepository.ObterIndicadorDisponibilidadeBBTSMultasDisponibilidade();
                    break;
                case DashboardViewEnum.DISPONIBILIDADE_TECNICOS:
                    viewDashboard.ViewDashboardDisponibilidadeTecnicos = this._dashboardRepository.ObterIndicadorDisponibilidadeTecnicos();
                    break;
                case DashboardViewEnum.DISPONIBILIDADE_TECNICOS_MEDIA_GLOBAL:
                    viewDashboard.ViewDashboardDisponibilidadeTecnicosMediaGlobal = this._dashboardRepository.ObterIndicadorDisponibilidadeTecnicosMediaGlobal();
                    break;
                case DashboardViewEnum.SPA:
                    viewDashboard.ViewDashboardSPA = this._dashboardRepository.ObterDadosSPA();
                    break;
                case DashboardViewEnum.SPA_TECNICOS_MENOR_DESEMPENHO:
                    viewDashboard.ViewDashboardSPATecnicosMenorDesempenho = this._dashboardRepository.ObterDadosSPATecnicosMenorDesempenho(parameters);
                    break;
                case DashboardViewEnum.SPA_TECNICOS_MAIOR_DESEMPENHO:
                    viewDashboard.ViewDashboardSPATecnicosMaiorDesempenho = this._dashboardRepository.ObterDadosSPATecnicosMaiorDesempenho(parameters);
                    break;
                case DashboardViewEnum.SLA_CLIENTES:
                    viewDashboard.ViewDashboardSLAClientes = this._dashboardRepository.ObterDadosSLAClientes();
                    break;
                case DashboardViewEnum.REINCIDENCIA_FILIAIS:
                    viewDashboard.ViewDashboardReincidenciaFiliais = this._dashboardRepository.ObterDadosReincidenciaFiliais();
                    break;
                case DashboardViewEnum.REINCIDENCIA_QUADRIMESTRE_FILIAIS:
                    viewDashboard.ViewDashboardReincidenciaQuadrimestreFiliais = this._dashboardRepository.ObterDadosReincidenciaQuadrimestreFilial(parameters);
                    break;                    
                case DashboardViewEnum.REINCIDENCIA_TECNICOS_MENOS_REINCIDENTES:
                    viewDashboard.ViewDashboardReincidenciaTecnicosMenosReincidentes = this._dashboardRepository.ObterDadosReincidenciaTecnicosMenosReincidentes();
                    break;
                case DashboardViewEnum.REINCIDENCIA_TECNICOS_MAIS_REINCIDENTES:
                    viewDashboard.ViewDashboardReincidenciaTecnicosMaisReincidentes = this._dashboardRepository.ObterDadosReincidenciaTecnicosMaisReincidentes();
                    break;
                case DashboardViewEnum.REINCIDENCIA_CLIENTES:
                    viewDashboard.ViewDashboardReincidenciaClientes = this._dashboardRepository.ObterDadosReincidenciaClientes();
                    break;
                case DashboardViewEnum.REINCIDENCIA_EQUIPAMENTOS_MAIS_REINCIDENTES:
                    viewDashboard.ViewDashboardEquipamentosMaisReincidentes = this._dashboardRepository.ObterDadosEquipamentosMaisReincidentes();
                    break;
                case DashboardViewEnum.PENDENCIA_FILIAIS:
                    viewDashboard.ViewDashboardPendenciaFiliais = this._dashboardRepository.ObterDadosPendenciaFiliais();
                    break;
                case DashboardViewEnum.PENDENCIA_QUADRIMESTRE_FILIAIS:
                    viewDashboard.ViewDashboardPendenciaQuadrimestreFiliais = this._dashboardRepository.ObterDadosPendenciaQuadrimestreFilial(parameters);
                    break;                    
                case DashboardViewEnum.PENDENCIA_TECNICOS_MENOS_PENDENCIA:
                    viewDashboard.ViewDashboardTecnicosMenosPendentes = this._dashboardRepository.ObterDadosTecnicosMenosPendentes();
                    break;
                case DashboardViewEnum.PENDENCIA_TECNICOS_MAIS_PENDENCIA:
                    viewDashboard.ViewDashboardTecnicosMaisPendentes = this._dashboardRepository.ObterDadosTecnicosMaisPendentes();
                    break;
                case DashboardViewEnum.PENDENCIA_GLOBAL:
                    viewDashboard.ViewDashboardPendenciaGlobal = this._dashboardRepository.ObterDadosPendenciaGlobal();
                    break;
                case DashboardViewEnum.PECAS_FALTANTES:
                    viewDashboard.ViewDashboardPecasFaltantes = this._dashboardRepository.ObterDadosPecasFaltantes();
                    break;
                case DashboardViewEnum.PECAS_MAIS_FALTANTES:
                    viewDashboard.ViewDashboardPecasMaisFaltantes = this._dashboardRepository.ObterDadosPecasMaisFaltantes();
                    break;
                case DashboardViewEnum.PECAS_CRITICAS_MAIS_FALTANTES:
                    viewDashboard.ViewDashboardPecasCriticasMaisFaltantes = this._dashboardRepository.ObterDadosPecasCriticasMaisFaltantes();
                    break;
                case DashboardViewEnum.PECAS_CRITICAS_CHAMADOS_FALTANTES:
                    viewDashboard.ViewDashboardPecasCriticaChamadosFaltantes = this._dashboardRepository.ObterDadosPecasCriticasChamadosFaltantes(parameters);
                    break;
                case DashboardViewEnum.PECAS_CRITICAS_ESTOQUE_FALTANTES:
                    viewDashboard.ViewDashboardPecasCriticaEstoqueFaltantes = this._dashboardRepository.ObterDadosPecasCriticasEstoqueFaltantes(parameters);
                    break;
                case DashboardViewEnum.DENSIDADE_EQUIPAMENTOS:
                    viewDashboard.ViewDashboardDensidadeEquipamentos = this._dashboardRepository.ObterDadosDensidadeEquipamentos(parameters);
                    break;
                case DashboardViewEnum.DENSIDADE_TECNICOS:
                    viewDashboard.ViewDashboardDensidadeTecnicos = this._dashboardRepository.ObterDadosDensidadeTecnicos(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_SLA_TECNICO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosSLATecnico = this._dashboardRepository.ObterDadosIndicadoresDetalhadosSLATecnico(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_SLA_REGIAO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosSLARegiao = this._dashboardRepository.ObterDadosIndicadoresDetalhadosSLARegiao(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_SLA_CLIENTE:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosSLACliente = this._dashboardRepository.ObterDadosIndicadoresDetalhadosSLACliente(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_PENDENCIA_CLIENTE:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosPendenciaCliente = this._dashboardRepository.ObterDadosIndicadoresDetalhadosPendenciaCliente(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_PENDENCIA_REGIAO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosPendenciaRegiao = this._dashboardRepository.ObterDadosIndicadoresDetalhadosPendenciaRegiao(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_PENDENCIA_TECNICO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosPendenciaTecnico = this._dashboardRepository.ObterDadosIndicadoresDetalhadosPendenciaTecnico(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_REINCIDENCIA_TECNICO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosReincidenciaTecnico = this._dashboardRepository.ObterDadosIndicadoresDetalhadosReincidenciaTecnico(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_REINCIDENCIA_CLIENTE:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosReincidenciaCliente = this._dashboardRepository.ObterDadosIndicadoresDetalhadosReincidenciaCliente(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_REINCIDENCIA_REGIAO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosReincidenciaRegiao = this._dashboardRepository.ObterDadosIndicadoresDetalhadosReincidenciaRegiao(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_PERFORMANCE:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosPerformance = this._dashboardRepository.ObterDadosIndicadoresDetalhadosPerformance(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_CHAMADOS_ANTIGOS:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosChamadosAntigos = this._dashboardRepository.ObterDadosIndicadoresDetalhadosChamadosAntigos(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_SPA_CLIENTE:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosSPACliente = this._dashboardRepository.ObterDadosIndicadoresDetalhadosSPACliente(parameters);
                    break;                    
                case DashboardViewEnum.INDICADORES_DETALHADOS_SPA_TECNICO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosSPATecnico = this._dashboardRepository.ObterDadosIndicadoresDetalhadosSPATecnico(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_SPA_REGIAO:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosSPARegiao = this._dashboardRepository.ObterDadosIndicadoresDetalhadosSPARegiao(parameters);
                    break;
                case DashboardViewEnum.INDICADORES_DETALHADOS_PRODUTIVIDADE:
                    viewDashboard.ViewDashboardIndicadoresDetalhadosProdutividade = this._dashboardRepository.ObterDadosIndicadoresDetalhadosProdutividade(parameters);
                    break;                    

                default:
                    break;
            }

            return viewDashboard;
        }
    }
}
