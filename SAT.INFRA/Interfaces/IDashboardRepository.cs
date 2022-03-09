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
        List<ViewDashboardSPATecnicosMenorDesempenho> ObterDadosSPATecnicosMenorDesempenho();
        List<ViewDashboardSPATecnicosMaiorDesempenho> ObterDadosSPATecnicosMaiorDesempenho();
        List<ViewDashboardSLAClientes> ObterDadosSLAClientes();
        List<ViewDashboardReincidenciaFiliais> ObterDadosReincidenciaFiliais();
        List<ViewDashboardReincidenciaTecnicosMenosReincidentes> ObterDadosReincidenciaTecnicosMenosReincidentes();
        List<ViewDashboardReincidenciaTecnicosMaisReincidentes> ObterDadosReincidenciaTecnicosMaisReincidentes();
        List<ViewDashboardEquipamentosMaisReincidentes> ObterDadosEquipamentosMaisReincidentes();
        List<ViewDashboardPendenciaFiliais> ObterDadosPendenciaFiliais();
        List<ViewDashboardTecnicosMenosPendentes> ObterDadosTecnicosMenosPendentes();
        List<ViewDashboardTecnicosMaisPendentes> ObterDadosTecnicosMaisPendentes();
        List<ViewDashboardReincidenciaClientes> ObterDadosReincidenciaClientes();
        List<ViewDashboardPendenciaGlobal> ObterDadosPendenciaGlobal();
        List<ViewDashboardPecasFaltantes> ObterDadosPecasFaltantes();
        List<ViewDashboardPecasMaisFaltantes> ObterDadosPecasMaisFaltantes();
        List<ViewDashboardPecasCriticasMaisFaltantes> ObterDadosPecasCriticasMaisFaltantes();
        List<ViewDashboardPecasCriticaChamadosFaltantes> ObterDadosPecasCriticasChamadosFaltantes(int codPeca);
        List<ViewDashboardPecasCriticaEstoqueFaltantes> ObterDadosPecasCriticasEstoqueFaltantes(int codPeca);
        List<ViewDashboardDensidadeEquipamentos> ObterDadosDensidadeEquipamentos();
        List<ViewDashboardDensidadeTecnicos> ObterDadosDensidadeTecnicos();
    }
}