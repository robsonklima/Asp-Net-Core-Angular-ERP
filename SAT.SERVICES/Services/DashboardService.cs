using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IFeriadoService _feriadoService;

        public DashboardService(IDashboardRepository dashboardRepository, IFeriadoService feriadoService)
        {
            this._dashboardRepository = dashboardRepository;
            this._feriadoService = feriadoService;
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
        public List<Indicador> ObterDadosIndicadorMaisRecente(string nomeIndicador)
        {
            return this._dashboardRepository.ObterDadosIndicadorMaisRecente(nomeIndicador);
        }

        public void Criar(string nomeIndicador, List<Indicador> indicadores, DateTime data)
        {
            this._dashboardRepository.Criar(nomeIndicador, indicadores, data);
        }

        public void Criar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data)
        {
            this._dashboardRepository.Criar(nomeIndicador, indicadores, data);
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(string nomeIndicador, DateTime data)
        {
            return this._dashboardRepository.ObterIndicadorDisponibilidadeTecnicos(nomeIndicador, data);
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDashboardTecnicoDisponibilidade(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            return this._dashboardRepository.ObterDadosDashboardTecnicoDisponibilidade(query, parameters, _feriadoService.GetDiasUteis);
        }

        public void Atualizar(string nomeIndicador, List<Indicador> indicadores, DateTime data)
        {
            this._dashboardRepository.Atualizar(nomeIndicador, indicadores, data);
        }

        public void Atualizar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data)
        {
            this._dashboardRepository.Atualizar(nomeIndicador, indicadores, data);
        }
    }
}
