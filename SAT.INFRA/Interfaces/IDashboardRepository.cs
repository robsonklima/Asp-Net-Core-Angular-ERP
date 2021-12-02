using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using System;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IDashboardRepository
    {
        PagedList<DashboardDisponibilidade> ObterPorParametros(DashboardParameters parameters);
        List<Indicador> ObterDadosIndicador(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim);
        List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim);
        void Criar(string nomeIndicador, List<Indicador> indicadores, DateTime data);
        void Criar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data);
    }
}