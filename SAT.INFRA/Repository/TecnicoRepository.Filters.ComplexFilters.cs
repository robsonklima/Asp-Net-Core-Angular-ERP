using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        public IQueryable<Tecnico> PeriodoAtendimentoFilter(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            // Filtro nas OS
            if (parameters.PeriodoMediaAtendInicio != DateTime.MinValue && parameters.PeriodoMediaAtendInicio != DateTime.MinValue)
            {
                query = query
                   .Include(t => t.OrdensServico.Where(os => os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                   os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim));
            }

            return query;
        }
    }
}
