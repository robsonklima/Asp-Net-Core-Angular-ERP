using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using SAT.MODELS.ViewModels;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca os dados da View em relação aos indicadores das filiais
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardIndicadoresFiliais> ObterDadosIndicadorFiliais()
        {
            return this._context.ViewDashboardIndicadoresFiliais.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação chamados abertos mais antigos corretivas
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardChamadosMaisAntigosCorretivas> ObterChamadosMaisAntigosCorretivas()
        {
            return this._context.ViewDashboardChamadosMaisAntigosCorretivas.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação chamados abertos mais antigos orçamentos
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardChamadosMaisAntigosOrcamentos> ObterChamadosMaisAntigosOrcamentos()
        {
            return this._context.ViewDashboardChamadosMaisAntigosOrcamentos.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Filiais
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDisponibilidadeBBTSFiliais> ObterIndicadorDisponibilidadeBBTSFiliais()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSFiliais.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Mapas das regiões
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDisponibilidadeBBTSMapaRegioes> ObterIndicadorDisponibilidadeBBTSMapaRegioes()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMapaRegioes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Multas
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> ObterIndicadorDisponibilidadeBBTSMultasDisponibilidade()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMultasDisponibilidade.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Multas por regiões
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDisponibilidadeBBTSMultasRegioes> ObterIndicadorDisponibilidadeBBTSMultasRegioes()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMultasRegioes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação a disponibilidade dos técnicos
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDisponibilidadeTecnicos> ObterIndicadorDisponibilidadeTecnicos()
        {
            return this._context.ViewDashboardDisponibilidadeTecnicos.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação a média global da disponibilidade dos técnicos
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDisponibilidadeTecnicosMediaGlobal> ObterIndicadorDisponibilidadeTecnicosMediaGlobal()
        {
            return this._context.ViewDashboardDisponibilidadeTecnicosMediaGlobal.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SPA
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardSPA> ObterDadosSPA()
        {
            return this._context.ViewDashboardSPA.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SLA dos Clientes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardSLAClientes> ObterDadosSLAClientes()
        {
            return this._context.ViewDashboardSLAClientes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia das filiais
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardReincidenciaFiliais> ObterDadosReincidenciaFiliais()
        {
            return this._context.ViewDashboardReincidenciaFiliais.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia dos clientes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardReincidenciaClientes> ObterDadosReincidenciaClientes()
        {
            return this._context.ViewDashboardReincidenciaClientes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SPA dos técnicos com menor desempenho
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardSPATecnicosMenorDesempenho> ObterDadosSPATecnicosMenorDesempenho()
        {
            return this._context.ViewDashboardSPATecnicosMenorDesempenho.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SPA dos técnicos com maior desempenho
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardSPATecnicosMaiorDesempenho> ObterDadosSPATecnicosMaiorDesempenho()
        {
            return this._context.ViewDashboardSPATecnicosMaiorDesempenho.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia dos técnicos menos reincidentes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardReincidenciaTecnicosMenosReincidentes> ObterDadosReincidenciaTecnicosMenosReincidentes()
        {
            return this._context.ViewDashboardReincidenciaTecnicosMenosReincidentes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia dos técnicos mais reincidentes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardReincidenciaTecnicosMaisReincidentes> ObterDadosReincidenciaTecnicosMaisReincidentes()
        {
            return this._context.ViewDashboardReincidenciaTecnicosMaisReincidentes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de equipamentos mais reincidentes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardEquipamentosMaisReincidentes> ObterDadosEquipamentosMaisReincidentes()
        {
            return this._context.ViewDashboardEquipamentosMaisReincidentes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação as pendencias das filiais
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardPendenciaFiliais> ObterDadosPendenciaFiliais()
        {
            return this._context.ViewDashboardPendenciaFiliais.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos tecnicos menos pendencias 
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardTecnicosMenosPendentes> ObterDadosTecnicosMenosPendentes()
        {
            return this._context.ViewDashboardTecnicosMenosPendentes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos tecnicos mais pendencias 
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardTecnicosMaisPendentes> ObterDadosTecnicosMaisPendentes()
        {
            return this._context.ViewDashboardTecnicosMaisPendentes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de pendencia global
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardPendenciaGlobal> ObterDadosPendenciaGlobal()
        {
            return this._context.ViewDashboardPendenciaGlobal.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados das peças faltantes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardPecasFaltantes> ObterDadosPecasFaltantes()
        {
            return this._context.ViewDashboardPecasFaltantes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados das peças mais faltantes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardPecasMaisFaltantes> ObterDadosPecasMaisFaltantes()
        {
            return this._context.ViewDashboardPecasMaisFaltantes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dadosdos chamados de peças mais faltantes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardPecasCriticasMaisFaltantes> ObterDadosPecasCriticasMaisFaltantes()
        {
            return this._context.ViewDashboardPecasCriticasMaisFaltantes.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de densidade de equipamentos
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardPecasCriticaChamadosFaltantes> ObterDadosPecasCriticasChamadosFaltantes(int codPeca)
        {
            return this._context.ViewDashboardPecasCriticaChamadosFaltantes.Where(cod => cod.CodPeca == codPeca).AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dadosdos chamados de peças mais faltantes
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardPecasCriticaEstoqueFaltantes> ObterDadosPecasCriticasEstoqueFaltantes(int codPeca)
        {
            return this._context.ViewDashboardPecasCriticaEstoqueFaltantes.Where(cod => cod.CodPeca == codPeca).AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de densidade de equipamentos
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDensidadeEquipamentos> ObterDadosDensidadeEquipamentos()
        {
            return this._context.ViewDashboardDensidadeEquipamentos.AsQueryable();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de densidade de tecnicos
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewDashboardDensidadeTecnicos> ObterDadosDensidadeTecnicos()
        {
            return this._context.ViewDashboardDensidadeTecnicos.AsQueryable();
        }
    }
}
