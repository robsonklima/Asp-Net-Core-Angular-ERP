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
    public class MRPLogixRepository : IMRPLogixRepository
    {
        private readonly AppDbContext _context;

        public MRPLogixRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(MRPLogix mprLogix)
        {
            _context.ChangeTracker.Clear();
            MRPLogix p = _context.MRPLogix.FirstOrDefault(p => p.CodMRPLogix == mprLogix.CodMRPLogix);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(mprLogix);
                _context.SaveChanges();
            }
        }

        public MRPLogix Criar(MRPLogix mprLogix)
        {
            _context.Add(mprLogix);
            _context.SaveChanges();
            return mprLogix;
        }

        public void Deletar(int codigo)
        {
            MRPLogix MRPLogix = _context.MRPLogix.FirstOrDefault(p => p.CodMRPLogix == codigo);

            if (MRPLogix != null)
            {
                _context.MRPLogix.Remove(MRPLogix);
                _context.SaveChanges();
            }
        }

        public MRPLogix ObterPorCodigo(int codigo)
        {
            return _context.MRPLogix
                .FirstOrDefault(p => p.CodMRPLogix == codigo);
        }

        public PagedList<MRPLogix> ObterPorParametros(MRPLogixParameters parameters)
        {
            var query = _context.MRPLogix
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodMRPLogix.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodMRPLogix.HasValue)
            {
                query = query.Where(q => q.CodMRPLogix == parameters.CodMRPLogix);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<MRPLogix>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
