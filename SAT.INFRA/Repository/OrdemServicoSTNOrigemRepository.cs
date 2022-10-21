using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class OrdemServicoSTNOrigemRepository : IOrdemServicoSTNOrigemRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoSTNOrigemRepository(AppDbContext context)
        {
            _context = context;
        }
        public OrdemServicoSTNOrigem ObterPorCodigo(int codOrigemChamadoSTN)
        {
            return _context.OrdemServicoSTNOrigem
                .FirstOrDefault(p => p.CodOrigemChamadoSTN == codOrigemChamadoSTN);
        }

        public PagedList<OrdemServicoSTNOrigem> ObterPorParametros(OrdemServicoSTNOrigemParameters parameters)
        {
            var query = _context.OrdemServicoSTNOrigem
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(o =>
                        o.CodOrigemChamadoSTN.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                        o.DescOrigemChamadoSTN.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodOrigemChamadoSTN.HasValue) {
                query = query.Where(o => o.CodOrigemChamadoSTN == parameters.CodOrigemChamadoSTN);
            }

            if (!string.IsNullOrWhiteSpace(parameters.DescOrigemChamadoSTN))
                query = query.Where(o => o.DescOrigemChamadoSTN == parameters.DescOrigemChamadoSTN);            

            if (parameters.IndAtivo.HasValue) {
                query = query.Where(e => e.IndAtivo == parameters.IndAtivo);
            }                

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrdemServicoSTNOrigem>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
