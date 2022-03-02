using System.Linq;

namespace SAT.MODELS.ViewModels
{
    public class ViewDadosDashboard
    {
        public IQueryable<ViewDashboardChamadosMaisAntigosCorretivas> ViewDashboardChamadosMaisAntigosCorretivas { get; set; }
        public IQueryable<ViewDashboardChamadosMaisAntigosOrcamentos> ViewDashboardChamadosMaisAntigosOrcamentos { get; set; }
        public IQueryable<ViewDashboardDisponibilidadeBBTSFiliais> ViewDashboardDisponibilidadeBBTSFiliais { get; set; }
        public IQueryable<ViewDashboardDisponibilidadeBBTSMapaRegioes> ViewDashboardDisponibilidadeBBTSMapaRegioes { get; set; }
        public IQueryable<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> ViewDashboardDisponibilidadeBBTSMultasDisponibilidade { get; set; }
        public IQueryable<ViewDashboardDisponibilidadeBBTSMultasRegioes> ViewDashboardDisponibilidadeBBTSMultasRegioes { get; set; }
        public IQueryable<ViewDashboardDisponibilidadeTecnicos> ViewDashboardDisponibilidadeTecnicos { get; set; }
        public IQueryable<ViewDashboardDisponibilidadeTecnicosMediaGlobal> ViewDashboardDisponibilidadeTecnicosMediaGlobal { get; set; }
        public IQueryable<ViewDashboardEquipamentosMaisReincidentes> ViewDashboardEquipamentosMaisReincidentes { get; set; }
        public IQueryable<ViewDashboardIndicadoresFiliais> ViewDashboardIndicadoresFiliais { get; set; }
        public IQueryable<ViewDashboardPecasCriticaChamadosFaltantes> ViewDashboardPecasCriticaChamadosFaltantes { get; set; }
        public IQueryable<ViewDashboardPecasCriticaEstoqueFaltantes> ViewDashboardPecasCriticaEstoqueFaltantes { get; set; }
        public IQueryable<ViewDashboardPecasCriticasMaisFaltantes> ViewDashboardPecasCriticasMaisFaltantes { get; set; }
        public IQueryable<ViewDashboardPecasFaltantes> ViewDashboardPecasFaltantes { get; set; }
        public IQueryable<ViewDashboardPecasMaisFaltantes> ViewDashboardPecasMaisFaltantes { get; set; }
        public IQueryable<ViewDashboardPendenciaFiliais> ViewDashboardPendenciaFiliais { get; set; }
        public IQueryable<ViewDashboardPendenciaGlobal> ViewDashboardPendenciaGlobal { get; set; }
        public IQueryable<ViewDashboardReincidenciaClientes> ViewDashboardReincidenciaClientes { get; set; }
        public IQueryable<ViewDashboardReincidenciaFiliais> ViewDashboardReincidenciaFiliais { get; set; }
        public IQueryable<ViewDashboardReincidenciaTecnicosMaisReincidentes> ViewDashboardReincidenciaTecnicosMaisReincidentes { get; set; }
        public IQueryable<ViewDashboardReincidenciaTecnicosMenosReincidentes> ViewDashboardReincidenciaTecnicosMenosReincidentes { get; set; }
        public IQueryable<ViewDashboardSLAClientes> ViewDashboardSLAClientes { get; set; }
        public IQueryable<ViewDashboardSPA> ViewDashboardSPA { get; set; }
        public IQueryable<ViewDashboardSPATecnicosMaiorDesempenho> ViewDashboardSPATecnicosMaiorDesempenho { get; set; }
        public IQueryable<ViewDashboardSPATecnicosMenorDesempenho> ViewDashboardSPATecnicosMenorDesempenho { get; set; }
        public IQueryable<ViewDashboardTecnicosMaisPendentes> ViewDashboardTecnicosMaisPendentes { get; set; }
        public IQueryable<ViewDashboardTecnicosMenosPendentes> ViewDashboardTecnicosMenosPendentes { get; set; }
        public IQueryable<ViewDashboardDensidadeEquipamentos> ViewDashboardDensidadeEquipamentos { get; set; }
        public IQueryable<ViewDashboardDensidadeTecnicos> ViewDashboardDensidadeTecnicos { get; set; }
    }
}
