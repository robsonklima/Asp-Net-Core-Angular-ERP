using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IDashboardRepository
    {
        List<Indicador> ObterDadosIndicador(string nomeIndicador, DateTime? dataInicio, DateTime? dataFim);
        List<Indicador> ObterDadosIndicadorMaisRecente(string nomeIndicador);
        List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(string nomeIndicador, DateTime data);
        List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDashboardTecnicoDisponibilidade(IQueryable<Tecnico> query, TecnicoParameters parameters, Func<DateTime, DateTime, int> FuncDiasUteis);
        void Criar(string nomeIndicador, List<Indicador> indicadores, DateTime data);
        void Criar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data);
        void Atualizar(string nomeIndicador, List<Indicador> indicadores, DateTime data);
        void Atualizar(string nomeIndicador, List<DashboardTecnicoDisponibilidadeTecnicoViewModel> indicadores, DateTime data);

    }
}