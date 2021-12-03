using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(IndicadorParameters parameters)
        {
            return _dashboardService.ObterIndicadorDisponibilidadeTecnicos(NomeIndicadorEnum.DISPONIBILIDADE_TECNICOS.Description(), parameters.DataFim);
        }

        /// <summary>
        /// Calculo sempre é feito num prazo de 30 dias
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDashboardTecnicoDisponibilidade(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            return _dashboardService.ObterDadosDashboardTecnicoDisponibilidade(query, parameters);
        }
    }
}