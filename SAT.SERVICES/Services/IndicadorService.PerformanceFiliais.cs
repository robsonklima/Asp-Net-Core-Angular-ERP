using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;

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