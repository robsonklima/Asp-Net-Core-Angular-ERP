using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class AdendoItemRepository : IAdendoItemRepository
    {
        private readonly AppDbContext _context;

        public AdendoItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public AdendoItem Atualizar(AdendoItem item)
        {
            _context.ChangeTracker.Clear();
            AdendoItem a = _context.AdendoItem.SingleOrDefault(a => a.CodAdendoItem == item.CodAdendoItem);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }

            return item;
        }

        public AdendoItem Criar(AdendoItem item)
        {
            try
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }

            return item;
        }

        public AdendoItem Deletar(int codigo)
        {
            AdendoItem a = _context.AdendoItem.SingleOrDefault(a => a.CodAdendoItem == codigo);

            if (a != null)
            {
                try
                {
                    _context.AdendoItem.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }

            return a;
        }

        public AdendoItem ObterPorCodigo(int codigo)
        {
            return _context.AdendoItem.SingleOrDefault(a => a.CodAdendoItem == codigo);
        }

        public PagedList<AdendoItem> ObterPorParametros(AdendoItemParameters parameters)
        {
            var query = _context.AdendoItem.AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    c =>
                    c.CodAdendoItem.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodEquipContrato.HasValue)
            {
                query = query.Where(a => a.CodEquipContrato == parameters.CodEquipContrato);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<AdendoItem>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
