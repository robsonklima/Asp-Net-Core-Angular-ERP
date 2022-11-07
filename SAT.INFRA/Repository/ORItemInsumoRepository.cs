using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ORItemInsumoRepository : IORItemInsumoRepository
    {
        private readonly AppDbContext _context;

        public ORItemInsumoRepository(AppDbContext context)
        {
            _context = context;
        }

        public ORItemInsumo Atualizar(ORItemInsumo insumo)
        {
            _context.ChangeTracker.Clear();
            ORItemInsumo p = _context.ORItemInsumo.FirstOrDefault(p => p.CodORItemInsumo == insumo.CodORItemInsumo);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(insumo);
                _context.SaveChanges();
            }

            return insumo;
        }

        public ORItemInsumo Criar(ORItemInsumo insumo)
        {
            _context.Add(insumo);
            _context.SaveChanges();
            return insumo;
        }

        public void Deletar(int cod)
        {
            ORItemInsumo insumo = _context.ORItemInsumo.FirstOrDefault(p => p.CodORItemInsumo == cod);

            if (insumo != null)
            {
                _context.ORItemInsumo.Remove(insumo);
                _context.SaveChanges();
            }
        }

        public ORItemInsumo ObterPorCodigo(int cod)
        {
            return _context.ORItemInsumo.FirstOrDefault(p => p.CodORItemInsumo == cod);
        }

        public PagedList<ORItemInsumo> ObterPorParametros(ORItemInsumoParameters parameters)
        {
            var query = _context.ORItemInsumo.AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodORItemInsumo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORItemInsumo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
