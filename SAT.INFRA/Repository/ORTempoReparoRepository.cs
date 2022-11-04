using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ORTempoReparoRepository : IORTempoReparoRepository
    {
        private readonly AppDbContext _context;

        public ORTempoReparoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORTempoReparo tr)
        {
            _context.ChangeTracker.Clear();
            ORTempoReparo p = _context.ORTempoReparo.FirstOrDefault(p => p.CodORTempoReparo == tr.CodORTempoReparo);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(tr);
                _context.SaveChanges();
            }
        }

        public void Criar(ORTempoReparo tr)
        {
            _context.Add(tr);
            _context.SaveChanges();
        }

        public void Deletar(int cod)
        {
            ORTempoReparo status = _context.ORTempoReparo.FirstOrDefault(p => p.CodORTempoReparo == cod);

            if (status != null)
            {
                _context.ORTempoReparo.Remove(status);
                _context.SaveChanges();
            }
        }

        public ORTempoReparo ObterPorCodigo(int cod)
        {
            return _context.ORTempoReparo.FirstOrDefault(p => p.CodORTempoReparo == cod);
        }

        public PagedList<ORTempoReparo> ObterPorParametros(ORTempoReparoParameters parameters)
        {
            var query = _context.ORTempoReparo.AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodORTempoReparo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORTempoReparo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
