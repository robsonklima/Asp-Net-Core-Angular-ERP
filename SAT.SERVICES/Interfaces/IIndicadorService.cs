﻿using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IIndicadorService
    {
        List<Indicador> ObterIndicadores(IndicadorParameters parameters);
        List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(IndicadorParameters parameters);
        void AtualizaDadosIndicadoresDashboard(DateTime periodoInicio, DateTime periodoFim);
    }
}
