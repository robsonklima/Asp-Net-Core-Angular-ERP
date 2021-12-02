using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        public List<Indicador> ObterDadosIndicador(IndicadorParameters parameters)
        {
            return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.PERFORMANCE_FILIALS.Description(),
                parameters.DataInicio, parameters.DataFim);
        }
    }
}