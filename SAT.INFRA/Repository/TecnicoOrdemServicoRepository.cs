using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace SAT.INFRA.Repositories
{
    public class TecnicoOrdemServicoRepository : ITecnicoOrdemServicoRepository
    {
        private readonly AppDbContext _context;

        public TecnicoOrdemServicoRepository()
        {
        }

        public TecnicoOrdemServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<Tecnico> ObterPorParametros(TecnicoOrdemServicoParameters parameters)
        {
            var tecnicos = _context.Tecnico
                .Include(t => t.Filial)
                .Include(t => t.Autorizada)
                .Include(t => t.TipoRota)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                tecnicos = tecnicos.Where(
                    t =>
                    t.CodTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Nome.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Autorizada.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Filial.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.IndAtivo != null)
            {
                tecnicos = tecnicos.Where(t => t.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.IndFerias != null)
            {
                tecnicos = tecnicos.Where(t => t.IndFerias == parameters.IndFerias);
            }

            if (parameters.CodFilial != null)
            {
                tecnicos = tecnicos.Where(t => t.CodFilial == parameters.CodFilial);
            }

            if (parameters.CodTecnico != null)
            {
                tecnicos = tecnicos.Where(t => t.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tecnicos = tecnicos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            if (parameters.CodigosStatusServico != null)
            {
                var codigosStatusServico = parameters.CodigosStatusServico.Split(',');

                tecnicos = tecnicos.Include(f => f.OrdensServico.Where(os => codigosStatusServico.Contains(os.CodStatusServico.ToString())));
            }

            return PagedList<Tecnico>.ToPagedList(tecnicos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
