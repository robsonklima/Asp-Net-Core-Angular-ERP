using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ORStatusRepository : IORStatusRepository
    {
        private readonly AppDbContext _context;

        public ORStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORStatus status)
        {
            _context.ChangeTracker.Clear();
            ORStatus p = _context.ORStatus.FirstOrDefault(p => p.CodStatus == status.CodStatus);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(status);
                _context.SaveChanges();
            }
        }

        public void Criar(ORStatus ORStatus)
        {
            _context.Add(ORStatus);
            _context.SaveChanges();
        }

        public void Deletar(int cod)
        {
            ORStatus status = _context.ORStatus.FirstOrDefault(p => p.CodStatus == cod);

            if (status != null)
            {
                _context.ORStatus.Remove(status);
                _context.SaveChanges();
            }
        }

        public ORStatus ObterPorCodigo(int cod)
        {
            return _context.ORStatus.FirstOrDefault(p => p.CodStatus == cod);
        }

        public PagedList<ORStatus> ObterPorParametros(ORStatusParameters parameters)
        {
            var query = _context.ORStatus.AsQueryable();


            if (!string.IsNullOrWhiteSpace(parameters.Abrev))
            {
                query = query.Where(o => o.Abrev == parameters.Abrev);
            }

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodStatus.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                            
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORStatus>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
