using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IDashboardRepository
    {
        List<ViewDashboardIndicadoresFiliais> ObterDadosIndicadorFiliais();
        List<ViewDashboardChamadosMaisAntigosCorretivas> ObterChamadosMaisAntigosCorretivas();
        List<ViewDashboardChamadosMaisAntigosOrcamentos> ObterChamadosMaisAntigosOrcamentos();
        List<ViewDashboardDisponibilidadeBBTSFiliais> ObterIndicadorDisponibilidadeBBTSFiliais();
        List<ViewDashboardDisponibilidadeBBTSMapaRegioes> ObterIndicadorDisponibilidadeBBTSMapaRegioes();
        List<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> ObterIndicadorDisponibilidadeBBTSMultasDisponibilidade();
        List<ViewDashboardDisponibilidadeBBTSMultasRegioes> ObterIndicadorDisponibilidadeBBTSMultasRegioes();
        List<ViewDashboardDisponibilidadeTecnicos> ObterIndicadorDisponibilidadeTecnicos();
        List<ViewDashboardDisponibilidadeTecnicosMediaGlobal> ObterIndicadorDisponibilidadeTecnicosMediaGlobal();
        List<ViewDashboardSPA> ObterDadosSPA();
        List<ViewDashboardSPATecnicosMenorDesempenho> ObterDadosSPATecnicosMenorDesempenho(DashboardParameters parameters);
        List<ViewDashboardSPATecnicosMaiorDesempenho> ObterDadosSPATecnicosMaiorDesempenho(DashboardParameters parameters);
        List<ViewDashboardSLAClientes> ObterDadosSLAClientes();
        List<ViewDashboardReincidenciaFiliais> ObterDadosReincidenciaFiliais();
        List<ViewDashboardReincidenciaQuadrimestreFiliais> ObterDadosReincidenciaQuadrimestreFilial(DashboardParameters parameters);
        List<ViewDashboardReincidenciaTecnicosMenosReincidentes> ObterDadosReincidenciaTecnicosMenosReincidentes();
        List<ViewDashboardReincidenciaTecnicosMaisReincidentes> ObterDadosReincidenciaTecnicosMaisReincidentes();
        List<ViewDashboardEquipamentosMaisReincidentes> ObterDadosEquipamentosMaisReincidentes();
        List<ViewDashboardPendenciaFiliais> ObterDadosPendenciaFiliais();
        List<ViewDashboardPendenciaQuadrimestreFiliais> ObterDadosPendenciaQuadrimestreFilial(DashboardParameters parameters);
        List<ViewDashboardTecnicosMenosPendentes> ObterDadosTecnicosMenosPendentes();
        List<ViewDashboardTecnicosMaisPendentes> ObterDadosTecnicosMaisPendentes();
        List<ViewDashboardReincidenciaClientes> ObterDadosReincidenciaClientes();
        List<ViewDashboardPendenciaGlobal> ObterDadosPendenciaGlobal();
        List<ViewDashboardPecasFaltantes> ObterDadosPecasFaltantes();
        List<ViewDashboardPecasMaisFaltantes> ObterDadosPecasMaisFaltantes();
        List<ViewDashboardPecasCriticasMaisFaltantes> ObterDadosPecasCriticasMaisFaltantes();
        List<ViewDashboardPecasCriticaChamadosFaltantes> ObterDadosPecasCriticasChamadosFaltantes(DashboardParameters parameters);
        List<ViewDashboardPecasCriticaEstoqueFaltantes> ObterDadosPecasCriticasEstoqueFaltantes(DashboardParameters parameters);
        List<ViewDashboardDensidadeEquipamentos> ObterDadosDensidadeEquipamentos(DashboardParameters parameters);
        List<ViewDashboardDensidadeTecnicos> ObterDadosDensidadeTecnicos(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosSLACliente> ObterDadosIndicadoresDetalhadosSLACliente(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosSLARegiao> ObterDadosIndicadoresDetalhadosSLARegiao(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosSLATecnico> ObterDadosIndicadoresDetalhadosSLATecnico(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosPendenciaTecnico> ObterDadosIndicadoresDetalhadosPendenciaTecnico(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosPendenciaRegiao> ObterDadosIndicadoresDetalhadosPendenciaRegiao(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosPendenciaCliente> ObterDadosIndicadoresDetalhadosPendenciaCliente(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosReincidenciaTecnico> ObterDadosIndicadoresDetalhadosReincidenciaTecnico(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosReincidenciaRegiao> ObterDadosIndicadoresDetalhadosReincidenciaRegiao(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosReincidenciaCliente> ObterDadosIndicadoresDetalhadosReincidenciaCliente(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosPerformance> ObterDadosIndicadoresDetalhadosPerformance(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosChamadosAntigos> ObterDadosIndicadoresDetalhadosChamadosAntigos(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosSPACliente> ObterDadosIndicadoresDetalhadosSPACliente(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosSPARegiao> ObterDadosIndicadoresDetalhadosSPARegiao(DashboardParameters parameters);
        List<ViewDashboardIndicadoresDetalhadosSPATecnico> ObterDadosIndicadoresDetalhadosSPATecnico(DashboardParameters parameters);        
    }
}