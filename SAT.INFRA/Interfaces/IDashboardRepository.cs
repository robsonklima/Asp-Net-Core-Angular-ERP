using SAT.MODELS.ViewModels;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IDashboardRepository
    {
        IQueryable<ViewDashboardIndicadoresFiliais> ObterDadosIndicadorFiliais();
        IQueryable<ViewDashboardChamadosMaisAntigosCorretivas> ObterChamadosMaisAntigosCorretivas();
        IQueryable<ViewDashboardChamadosMaisAntigosOrcamentos> ObterChamadosMaisAntigosOrcamentos();
        IQueryable<ViewDashboardDisponibilidadeBBTSFiliais> ObterIndicadorDisponibilidadeBBTSFiliais();
        IQueryable<ViewDashboardDisponibilidadeBBTSMapaRegioes> ObterIndicadorDisponibilidadeBBTSMapaRegioes();
        IQueryable<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> ObterIndicadorDisponibilidadeBBTSMultasDisponibilidade();
        IQueryable<ViewDashboardDisponibilidadeBBTSMultasRegioes> ObterIndicadorDisponibilidadeBBTSMultasRegioes();
        IQueryable<ViewDashboardDisponibilidadeTecnicos> ObterIndicadorDisponibilidadeTecnicos();
        IQueryable<ViewDashboardDisponibilidadeTecnicosMediaGlobal> ObterIndicadorDisponibilidadeTecnicosMediaGlobal();
        IQueryable<ViewDashboardSPA> ObterDadosSPA();
        IQueryable<ViewDashboardSPATecnicosMenorDesempenho> ObterDadosSPATecnicosMenorDesempenho();
        IQueryable<ViewDashboardSPATecnicosMaiorDesempenho> ObterDadosSPATecnicosMaiorDesempenho();
        IQueryable<ViewDashboardSLAClientes> ObterDadosSLAClientes();
        IQueryable<ViewDashboardReincidenciaFiliais> ObterDadosReincidenciaFiliais();
        IQueryable<ViewDashboardReincidenciaTecnicosMenosReincidentes> ObterDadosReincidenciaTecnicosMenosReincidentes();
        IQueryable<ViewDashboardReincidenciaTecnicosMaisReincidentes> ObterDadosReincidenciaTecnicosMaisReincidentes();
        IQueryable<ViewDashboardEquipamentosMaisReincidentes> ObterDadosEquipamentosMaisReincidentes();
        IQueryable<ViewDashboardPendenciaFiliais> ObterDadosPendenciaFiliais();
        IQueryable<ViewDashboardTecnicosMenosPendentes> ObterDadosTecnicosMenosPendentes();
        IQueryable<ViewDashboardTecnicosMaisPendentes> ObterDadosTecnicosMaisPendentes();
        IQueryable<ViewDashboardReincidenciaClientes> ObterDadosReincidenciaClientes();
        IQueryable<ViewDashboardPendenciaGlobal> ObterDadosPendenciaGlobal();
        IQueryable<ViewDashboardPecasFaltantes> ObterDadosPecasFaltantes();
        IQueryable<ViewDashboardPecasMaisFaltantes> ObterDadosPecasMaisFaltantes();
        IQueryable<ViewDashboardPecasCriticasMaisFaltantes> ObterDadosPecasCriticasMaisFaltantes();
        IQueryable<ViewDashboardPecasCriticaChamadosFaltantes> ObterDadosPecasCriticasChamadosFaltantes(int codPeca);
        IQueryable<ViewDashboardPecasCriticaEstoqueFaltantes> ObterDadosPecasCriticasEstoqueFaltantes(int codPeca);
        IQueryable<ViewDashboardDensidadeEquipamentos> ObterDadosDensidadeEquipamentos();
        IQueryable<ViewDashboardDensidadeTecnicos> ObterDadosDensidadeTecnicos();
    }
}