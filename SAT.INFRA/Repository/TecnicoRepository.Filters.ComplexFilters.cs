using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Entities.Params;
using System;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        public IQueryable<Tecnico> PeriodoAtendimentoFilter(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            // Filtro nas OS
            if (parameters.PeriodoMediaAtendInicio != DateTime.MinValue && parameters.PeriodoMediaAtendFim != DateTime.MinValue)
            {
                query = query.Include(t => t.OrdensServico.Where(os => os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                                                    os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim))
                   .ThenInclude(r => r.RelatoriosAtendimento);
            }

            return query;
        }
    }
}
