using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Interfaces
{
    public interface IDashboardService
    {
        List<DashboardDisponibilidade> ObterDadosDashboard(DashboardParameters parameters);
        List<Indicador> ObterDadosIndicador(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim);
        List<Indicador> ObterDadosIndicadorMaisRecente(string nomeIndicador);
        List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(string nomeIndicador, DateTime data);
        List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDashboardTecnicoDisponibilidade(IQueryable<Tecnico> query, TecnicoParameters parameters);
        void Criar(string nomeIndicador, List<Indicador> indicadores, DateTime data);
        void Criar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data);
        void Atualizar(string nomeIndicador, List<Indicador> indicadores, DateTime data);
        void Atualizar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data);
    }
}
