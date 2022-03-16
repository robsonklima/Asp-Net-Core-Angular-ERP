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
        List<ViewDashboardReincidenciaQuadrimestreFiliais> ObterDadosReincidenciaQuadrimestreFilial(int codFilial);
        List<ViewDashboardReincidenciaTecnicosMenosReincidentes> ObterDadosReincidenciaTecnicosMenosReincidentes();
        List<ViewDashboardReincidenciaTecnicosMaisReincidentes> ObterDadosReincidenciaTecnicosMaisReincidentes();
        List<ViewDashboardEquipamentosMaisReincidentes> ObterDadosEquipamentosMaisReincidentes();
        List<ViewDashboardPendenciaFiliais> ObterDadosPendenciaFiliais();
        List<ViewDashboardPendenciaQuadrimestreFiliais> ObterDadosPendenciaQuadrimestreFilial(int codFilial);
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
        List<ViewDashboardIndicadoresDetalhadosSLACliente> ObterDadosIndicadoresDetalhadosSLACliente(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosSLARegiao> ObterDadosIndicadoresDetalhadosSLARegiao(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosSLATecnico> ObterDadosIndicadoresDetalhadosSLATecnico(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosPendenciaTecnico> ObterDadosIndicadoresDetalhadosPendenciaTecnico(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosPendenciaRegiao> ObterDadosIndicadoresDetalhadosPendenciaRegiao(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosPendenciaCliente> ObterDadosIndicadoresDetalhadosPendenciaCliente(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosReincidenciaTecnico> ObterDadosIndicadoresDetalhadosReincidenciaTecnico(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosReincidenciaRegiao> ObterDadosIndicadoresDetalhadosReincidenciaRegiao(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosReincidenciaCliente> ObterDadosIndicadoresDetalhadosReincidenciaCliente(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosPerformance> ObterDadosIndicadoresDetalhadosPerformance(int codFilial);
        List<ViewDashboardIndicadoresDetalhadosChamadosAntigos> ObterDadosIndicadoresDetalhadosChamadosAntigos(int codFilial);
    }
}