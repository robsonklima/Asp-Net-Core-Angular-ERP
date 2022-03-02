using SAT.INFRA.Interfaces;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.MODELS.ViewModels.Dashboard;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository, IFeriadoService feriadoService)
        {
            this._dashboardRepository = dashboardRepository;
        }

        public ViewDadosDashboard ObterViewPorParametros(ViewDadosDashboardParameters viewDadosDashboardParameters)
        {
            ViewDadosDashboard viewDashboard = new();

            switch (viewDadosDashboardParameters.DashboardViewEnum)
            {
                // Tela #1
                case DashboardViewEnum.INDICADORES_FILIAL:
                    viewDashboard.ViewDashboardIndicadoresFiliais = this._dashboardRepository.ObterDadosIndicadorFiliais();
                    break;
                case DashboardViewEnum.CHAMADOS_ANTIGOS_CORRETIVAS:
                    viewDashboard.ViewDashboardChamadosMaisAntigosCorretivas = this._dashboardRepository.ObterChamadosMaisAntigosCorretivas();
                    break;
                case DashboardViewEnum.CHAMADOS_ANTIGOS_ORCAMENTOS:
                    viewDashboard.ViewDashboardChamadosMaisAntigosOrcamentos = this._dashboardRepository.ObterChamadosMaisAntigosOrcamentos();
                    break;
                // Tela #1

                // Tela #2
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
                // Tela #2

                // Tela #3
                case DashboardViewEnum.DISPONIBILIDADE_TECNICOS:
                    viewDashboard.ViewDashboardDisponibilidadeTecnicos = this._dashboardRepository.ObterIndicadorDisponibilidadeTecnicos();
                    break;
                case DashboardViewEnum.DISPONIBILIDADE_TECNICOS_MEDIA_GLOBAL:
                    viewDashboard.ViewDashboardDisponibilidadeTecnicosMediaGlobal = this._dashboardRepository.ObterIndicadorDisponibilidadeTecnicosMediaGlobal();
                    break;
                // Tela #3

                // Tela #4
                case DashboardViewEnum.SPA:
                    viewDashboard.ViewDashboardSPA = this._dashboardRepository.ObterDadosSPA();
                    break;
                case DashboardViewEnum.SPA_TECNICOS_MENOR_DESEMPENHO:
                    viewDashboard.ViewDashboardSPATecnicosMenorDesempenho = this._dashboardRepository.ObterDadosSPATecnicosMenorDesempenho();
                    break;
                case DashboardViewEnum.SPA_TECNICOS_MAIOR_DESEMPENHO:
                    viewDashboard.ViewDashboardSPATecnicosMaiorDesempenho = this._dashboardRepository.ObterDadosSPATecnicosMaiorDesempenho();
                    break;
                // Tela #4

                // Tela #5
                case DashboardViewEnum.SLA_CLIENTES:
                    viewDashboard.ViewDashboardSLAClientes = this._dashboardRepository.ObterDadosSLAClientes();
                    break;
                // Tela #5

                // Tela #6
                case DashboardViewEnum.REINCIDENCIA_FILIAIS:
                    viewDashboard.ViewDashboardReincidenciaFiliais = this._dashboardRepository.ObterDadosReincidenciaFiliais();
                    break;
                case DashboardViewEnum.REINCIDENCIA_TECNICOS_MENOS_REINCIDENTES:
                    viewDashboard.ViewDashboardReincidenciaTecnicosMenosReincidentes = this._dashboardRepository.ObterDadosReincidenciaTecnicosMenosReincidentes();
                    break;
                case DashboardViewEnum.REINCIDENCIA_TECNICOS_MAIS_REINCIDENTES:
                    viewDashboard.ViewDashboardReincidenciaTecnicosMaisReincidentes = this._dashboardRepository.ObterDadosReincidenciaTecnicosMaisReincidentes();
                    break;
                // Tela #6

                // Tela #7
                case DashboardViewEnum.REINCIDENCIA_CLIENTES:
                    viewDashboard.ViewDashboardReincidenciaClientes = this._dashboardRepository.ObterDadosReincidenciaClientes();
                    break;
                case DashboardViewEnum.REINCIDENCIA_EQUIPAMENTOS_MAIS_REINCIDENTES:
                    viewDashboard.ViewDashboardEquipamentosMaisReincidentes = this._dashboardRepository.ObterDadosEquipamentosMaisReincidentes();
                    break;
                // Tela #7

                // Tela #8
                case DashboardViewEnum.PENDENCIA_FILIAIS:
                    viewDashboard.ViewDashboardPendenciaFiliais = this._dashboardRepository.ObterDadosPendenciaFiliais();
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
                // Tela #8

                // Tela #9
                case DashboardViewEnum.PECAS_FALTANTES:
                    viewDashboard.ViewDashboardPecasFaltantes = this._dashboardRepository.ObterDadosPecasFaltantes();
                    break;
                case DashboardViewEnum.PECAS_MAIS_FALTANTES:
                    viewDashboard.ViewDashboardPecasMaisFaltantes = this._dashboardRepository.ObterDadosPecasMaisFaltantes();
                    break;
                // Tela #9

                // Tela #9
                case DashboardViewEnum.PECAS_CRITICAS_MAIS_FALTANTES:
                    viewDashboard.ViewDashboardPecasCriticasMaisFaltantes = this._dashboardRepository.ObterDadosPecasCriticasMaisFaltantes();
                    break;
                case DashboardViewEnum.PECAS_CRITICAS_CHAMADOS_FALTANTES:
                    viewDashboard.ViewDashboardPecasCriticaChamadosFaltantes = this._dashboardRepository.ObterDadosPecasCriticasChamadosFaltantes(viewDadosDashboardParameters.CodPeca.Value);
                    break;
                case DashboardViewEnum.PECAS_CRITICAS_ESTOQUE_FALTANTES:
                    viewDashboard.ViewDashboardPecasCriticaEstoqueFaltantes = this._dashboardRepository.ObterDadosPecasCriticasEstoqueFaltantes(viewDadosDashboardParameters.CodPeca.Value);
                    break;
                // Tela #9

                // Tela #10
                case DashboardViewEnum.DENSIDADE_EQUIPAMENTOS:
                    viewDashboard.ViewDashboardDensidadeEquipamentos = this._dashboardRepository.ObterDadosDensidadeEquipamentos();
                    break;
                case DashboardViewEnum.DENSIDADE_TECNICOS:
                    viewDashboard.ViewDashboardDensidadeTecnicos = this._dashboardRepository.ObterDadosDensidadeTecnicos();
                    break;
                // Tela #10

                default:
                    break;
            }

            return viewDashboard;
        }
    }
}
