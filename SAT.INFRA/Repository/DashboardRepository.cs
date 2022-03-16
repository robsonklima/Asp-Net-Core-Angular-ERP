using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<ViewDashboardIndicadoresFiliais> ObterDadosIndicadorFiliais()
        {
            return this._context.ViewDashboardIndicadoresFiliais.ToList();
        }

        public List<ViewDashboardChamadosMaisAntigosCorretivas> ObterChamadosMaisAntigosCorretivas()
        {
            return this._context.ViewDashboardChamadosMaisAntigosCorretivas.ToList();
        }

        public List<ViewDashboardChamadosMaisAntigosOrcamentos> ObterChamadosMaisAntigosOrcamentos()
        {
            return this._context.ViewDashboardChamadosMaisAntigosOrcamentos.ToList();
        }

        public List<ViewDashboardDisponibilidadeBBTSFiliais> ObterIndicadorDisponibilidadeBBTSFiliais()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSFiliais.ToList();
        }

        public List<ViewDashboardDisponibilidadeBBTSMapaRegioes> ObterIndicadorDisponibilidadeBBTSMapaRegioes()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMapaRegioes.ToList();
        }

        public List<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> ObterIndicadorDisponibilidadeBBTSMultasDisponibilidade()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMultasDisponibilidade.ToList();
        }

        public List<ViewDashboardDisponibilidadeBBTSMultasRegioes> ObterIndicadorDisponibilidadeBBTSMultasRegioes()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMultasRegioes.ToList();
        }

        public List<ViewDashboardDisponibilidadeTecnicos> ObterIndicadorDisponibilidadeTecnicos()
        {
            return this._context.ViewDashboardDisponibilidadeTecnicos.ToList();
        }

        public List<ViewDashboardDisponibilidadeTecnicosMediaGlobal> ObterIndicadorDisponibilidadeTecnicosMediaGlobal()
        {
            return this._context.ViewDashboardDisponibilidadeTecnicosMediaGlobal.ToList();
        }

        public List<ViewDashboardSPA> ObterDadosSPA()
        {
            return this._context.ViewDashboardSPA.ToList();
        }

        public List<ViewDashboardSLAClientes> ObterDadosSLAClientes()
        {
            return this._context.ViewDashboardSLAClientes.ToList();
        }

        public List<ViewDashboardReincidenciaFiliais> ObterDadosReincidenciaFiliais()
        {
            return this._context.ViewDashboardReincidenciaFiliais.ToList();
        }

        public List<ViewDashboardReincidenciaQuadrimestreFiliais> ObterDadosReincidenciaQuadrimestreFiliais(int CodFilial)
        {
            return this._context.ViewDashboardReincidenciaQuadrimestreFiliais.Where(cod => cod.CodFilial == CodFilial).ToList();
        }        

        public List<ViewDashboardReincidenciaClientes> ObterDadosReincidenciaClientes()
        {
            return this._context.ViewDashboardReincidenciaClientes.ToList();
        }

        public List<ViewDashboardSPATecnicosMenorDesempenho> ObterDadosSPATecnicosMenorDesempenho()
        {
            return this._context.ViewDashboardSPATecnicosMenorDesempenho.ToList();
        }

        public List<ViewDashboardSPATecnicosMaiorDesempenho> ObterDadosSPATecnicosMaiorDesempenho()
        {
            return this._context.ViewDashboardSPATecnicosMaiorDesempenho.ToList();
        }

        public List<ViewDashboardReincidenciaTecnicosMenosReincidentes> ObterDadosReincidenciaTecnicosMenosReincidentes()
        {
            return this._context.ViewDashboardReincidenciaTecnicosMenosReincidentes.ToList();
        }

        public List<ViewDashboardReincidenciaTecnicosMaisReincidentes> ObterDadosReincidenciaTecnicosMaisReincidentes()
        {
            return this._context.ViewDashboardReincidenciaTecnicosMaisReincidentes.ToList();
        }

        public List<ViewDashboardEquipamentosMaisReincidentes> ObterDadosEquipamentosMaisReincidentes()
        {
            return this._context.ViewDashboardEquipamentosMaisReincidentes.ToList();
        }

        public List<ViewDashboardPendenciaFiliais> ObterDadosPendenciaFiliais()
        {
            return this._context.ViewDashboardPendenciaFiliais.ToList();
        }

        public List<ViewDashboardPendenciaQuadrimestreFiliais> ObterDadosPendenciaQuadrimestreFiliais(int CodFilial)
        {
            return this._context.ViewDashboardPendenciaQuadrimestreFiliais.Where(cod => cod.CodFilial == CodFilial).ToList();
        }

        public List<ViewDashboardTecnicosMenosPendentes> ObterDadosTecnicosMenosPendentes()
        {
            return this._context.ViewDashboardTecnicosMenosPendentes.ToList();
        }

        public List<ViewDashboardTecnicosMaisPendentes> ObterDadosTecnicosMaisPendentes()
        {
            return this._context.ViewDashboardTecnicosMaisPendentes.ToList();
        }

        public List<ViewDashboardPendenciaGlobal> ObterDadosPendenciaGlobal()
        {
            return this._context.ViewDashboardPendenciaGlobal.ToList();
        }

        public List<ViewDashboardPecasFaltantes> ObterDadosPecasFaltantes()
        {
            return this._context.ViewDashboardPecasFaltantes.ToList();
        }

        public List<ViewDashboardPecasMaisFaltantes> ObterDadosPecasMaisFaltantes()
        {
            return this._context.ViewDashboardPecasMaisFaltantes.ToList();
        }

        public List<ViewDashboardPecasCriticasMaisFaltantes> ObterDadosPecasCriticasMaisFaltantes()
        {
            return this._context.ViewDashboardPecasCriticasMaisFaltantes.ToList();
        }

        public List<ViewDashboardPecasCriticaChamadosFaltantes> ObterDadosPecasCriticasChamadosFaltantes(int codPeca)
        {
            return this._context.ViewDashboardPecasCriticaChamadosFaltantes.Where(cod => cod.CodPeca == codPeca).ToList();
        }

        public List<ViewDashboardPecasCriticaEstoqueFaltantes> ObterDadosPecasCriticasEstoqueFaltantes(int codPeca)
        {
            return this._context.ViewDashboardPecasCriticaEstoqueFaltantes.Where(cod => cod.CodPeca == codPeca).ToList();
        }

        public List<ViewDashboardDensidadeEquipamentos> ObterDadosDensidadeEquipamentos()
        {
            return this._context.ViewDashboardDensidadeEquipamentos.ToList();
        }

        public List<ViewDashboardDensidadeTecnicos> ObterDadosDensidadeTecnicos()
        {
            return this._context.ViewDashboardDensidadeTecnicos.ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosSLACliente> ObterDadosIndicadoresDetalhadosSLACliente(int CodFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosSLACliente.Where(cod => cod.CodFilial == CodFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosSLARegiao> ObterDadosIndicadoresDetalhadosSLARegiao(int CodFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosSLARegiao.Where(cod => cod.CodFilial == CodFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosSLATecnico> ObterDadosIndicadoresDetalhadosSLATecnico(int CodFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosSLATecnico.Where(cod => cod.CodFilial == CodFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosPendenciaTecnico> ObterDadosIndicadoresDetalhadosPendenciaTecnico(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosPendenciaTecnico.Where(cod => cod.CodFilial == codFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosPendenciaRegiao> ObterDadosIndicadoresDetalhadosPendenciaRegiao(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosPendenciaRegiao.Where(cod => cod.CodFilial == codFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosPendenciaCliente> ObterDadosIndicadoresDetalhadosPendenciaCliente(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosPendenciaCliente.Where(cod => cod.CodFilial == codFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosReincidenciaTecnico> ObterDadosIndicadoresDetalhadosReincidenciaTecnico(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosReincidenciaTecnico.Where(cod => cod.CodFilial == codFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosReincidenciaRegiao> ObterDadosIndicadoresDetalhadosReincidenciaRegiao(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosReincidenciaRegiao.Where(cod => cod.CodFilial == codFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosReincidenciaCliente> ObterDadosIndicadoresDetalhadosReincidenciaCliente(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosReincidenciaCliente.Where(cod => cod.CodFilial == codFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosPerformance> ObterDadosIndicadoresDetalhadosPerformance(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosPerformance.Where(cod => cod.CodFilial == codFilial).ToList();
        }

        public List<ViewDashboardIndicadoresDetalhadosChamadosAntigos> ObterDadosIndicadoresDetalhadosChamadosAntigos(int codFilial)
        {
            return this._context.ViewDashboardIndicadoresDetalhadosChamadosAntigos.Where(cod => cod.CodFilial == codFilial).ToList();
        }
    }
}
