using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public partial class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            this._dashboardRepository = dashboardRepository;
        }

        public List<DashboardDisponibilidade> ObterDadosDashboard(DashboardParameters parameters)
        {
            switch (parameters.Tipo)
            {
                default:
                    return null;
            }
        }

        public List<Indicador> ObterDadosIndicador(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim)
        {
            return this._dashboardRepository.ObterDadosIndicador(nomeIndicador, dataInicio, dataFim);
        }

        public void Criar(string nomeIndicador, List<Indicador> indicadores, DateTime data)
        {
            this._dashboardRepository.Criar(nomeIndicador, indicadores, data);
        }

        public void Criar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data)
        {
            this._dashboardRepository.Criar(nomeIndicador, indicadores, data);
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim)
        {
            return this._dashboardRepository.ObterIndicadorDisponibilidadeTecnicos(nomeIndicador, dataInicio, dataFim);
        }
    }
}
