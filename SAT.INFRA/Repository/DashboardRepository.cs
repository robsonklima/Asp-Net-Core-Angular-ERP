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

        /// <summary>
        /// Busca os dados da View em relação aos indicadores das filiais
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardIndicadoresFiliais> ObterDadosIndicadorFiliais()
        {
            return this._context.ViewDashboardIndicadoresFiliais.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação chamados abertos mais antigos corretivas
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardChamadosMaisAntigosCorretivas> ObterChamadosMaisAntigosCorretivas()
        {
            return this._context.ViewDashboardChamadosMaisAntigosCorretivas.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação chamados abertos mais antigos orçamentos
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardChamadosMaisAntigosOrcamentos> ObterChamadosMaisAntigosOrcamentos()
        {
            return this._context.ViewDashboardChamadosMaisAntigosOrcamentos.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Filiais
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDisponibilidadeBBTSFiliais> ObterIndicadorDisponibilidadeBBTSFiliais()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSFiliais.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Mapas das regiões
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDisponibilidadeBBTSMapaRegioes> ObterIndicadorDisponibilidadeBBTSMapaRegioes()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMapaRegioes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Multas
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> ObterIndicadorDisponibilidadeBBTSMultasDisponibilidade()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMultasDisponibilidade.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação a BBTS Multas por regiões
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDisponibilidadeBBTSMultasRegioes> ObterIndicadorDisponibilidadeBBTSMultasRegioes()
        {
            return this._context.ViewDashboardDisponibilidadeBBTSMultasRegioes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação a disponibilidade dos técnicos
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDisponibilidadeTecnicos> ObterIndicadorDisponibilidadeTecnicos()
        {
            return this._context.ViewDashboardDisponibilidadeTecnicos.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação a média global da disponibilidade dos técnicos
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDisponibilidadeTecnicosMediaGlobal> ObterIndicadorDisponibilidadeTecnicosMediaGlobal()
        {
            return this._context.ViewDashboardDisponibilidadeTecnicosMediaGlobal.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SPA
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardSPA> ObterDadosSPA()
        {
            return this._context.ViewDashboardSPA.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SLA dos Clientes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardSLAClientes> ObterDadosSLAClientes()
        {
            return this._context.ViewDashboardSLAClientes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia das filiais
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardReincidenciaFiliais> ObterDadosReincidenciaFiliais()
        {
            return this._context.ViewDashboardReincidenciaFiliais.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia dos clientes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardReincidenciaClientes> ObterDadosReincidenciaClientes()
        {
            return this._context.ViewDashboardReincidenciaClientes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SPA dos técnicos com menor desempenho
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardSPATecnicosMenorDesempenho> ObterDadosSPATecnicosMenorDesempenho()
        {
            return this._context.ViewDashboardSPATecnicosMenorDesempenho.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de SPA dos técnicos com maior desempenho
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardSPATecnicosMaiorDesempenho> ObterDadosSPATecnicosMaiorDesempenho()
        {
            return this._context.ViewDashboardSPATecnicosMaiorDesempenho.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia dos técnicos menos reincidentes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardReincidenciaTecnicosMenosReincidentes> ObterDadosReincidenciaTecnicosMenosReincidentes()
        {
            return this._context.ViewDashboardReincidenciaTecnicosMenosReincidentes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de reincidencia dos técnicos mais reincidentes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardReincidenciaTecnicosMaisReincidentes> ObterDadosReincidenciaTecnicosMaisReincidentes()
        {
            return this._context.ViewDashboardReincidenciaTecnicosMaisReincidentes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de equipamentos mais reincidentes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardEquipamentosMaisReincidentes> ObterDadosEquipamentosMaisReincidentes()
        {
            return this._context.ViewDashboardEquipamentosMaisReincidentes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação as pendencias das filiais
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardPendenciaFiliais> ObterDadosPendenciaFiliais()
        {
            return this._context.ViewDashboardPendenciaFiliais.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos tecnicos menos pendencias 
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardTecnicosMenosPendentes> ObterDadosTecnicosMenosPendentes()
        {
            return this._context.ViewDashboardTecnicosMenosPendentes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos tecnicos mais pendencias 
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardTecnicosMaisPendentes> ObterDadosTecnicosMaisPendentes()
        {
            return this._context.ViewDashboardTecnicosMaisPendentes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de pendencia global
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardPendenciaGlobal> ObterDadosPendenciaGlobal()
        {
            return this._context.ViewDashboardPendenciaGlobal.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados das peças faltantes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardPecasFaltantes> ObterDadosPecasFaltantes()
        {
            return this._context.ViewDashboardPecasFaltantes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados das peças mais faltantes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardPecasMaisFaltantes> ObterDadosPecasMaisFaltantes()
        {
            return this._context.ViewDashboardPecasMaisFaltantes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dadosdos chamados de peças mais faltantes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardPecasCriticasMaisFaltantes> ObterDadosPecasCriticasMaisFaltantes()
        {
            return this._context.ViewDashboardPecasCriticasMaisFaltantes.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de densidade de equipamentos
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardPecasCriticaChamadosFaltantes> ObterDadosPecasCriticasChamadosFaltantes(int codPeca)
        {
            return this._context.ViewDashboardPecasCriticaChamadosFaltantes.Where(cod => cod.CodPeca == codPeca).ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dadosdos chamados de peças mais faltantes
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardPecasCriticaEstoqueFaltantes> ObterDadosPecasCriticasEstoqueFaltantes(int codPeca)
        {
            return this._context.ViewDashboardPecasCriticaEstoqueFaltantes.Where(cod => cod.CodPeca == codPeca).ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de densidade de equipamentos
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDensidadeEquipamentos> ObterDadosDensidadeEquipamentos()
        {
            return this._context.ViewDashboardDensidadeEquipamentos.ToList();
        }

        /// <summary>
        /// Busca os dados da View em relação aos dados de densidade de tecnicos
        /// </summary>
        /// <returns></returns>
        public List<ViewDashboardDensidadeTecnicos> ObterDadosDensidadeTecnicos()
        {
            return this._context.ViewDashboardDensidadeTecnicos.ToList();
        }
    }
}
