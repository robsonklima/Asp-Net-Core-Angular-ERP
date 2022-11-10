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
    public class ItemDefeitoRepository : IItemDefeitoRepository
    {
        private readonly AppDbContext _context;

        public ItemDefeitoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ItemDefeito itemDefeito)
        {
            _context.ChangeTracker.Clear();
            ItemDefeito p = _context.ItemDefeito.FirstOrDefault(p => p.CodItemDefeito == itemDefeito.CodItemDefeito);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(itemDefeito);
                _context.SaveChanges();
            }
        }

        public ItemDefeito Criar(ItemDefeito itemDefeito)
        {
            _context.Add(itemDefeito);
            _context.SaveChanges();
            return itemDefeito;
        }

        public void Deletar(int codigo)
        {
            ItemDefeito ItemDefeito = _context.ItemDefeito.FirstOrDefault(p => p.CodItemDefeito == codigo);

            if (ItemDefeito != null)
            {
                _context.ItemDefeito.Remove(ItemDefeito);
                _context.SaveChanges();
            }
        }

        public ItemDefeito ObterPorCodigo(int codigo)
        {
            return _context.ItemDefeito
                .Include(or => or.ORDefeito)
                .Include(or => or.ORItem)
                .Include(or => or.Usuario)
                .FirstOrDefault(p => p.CodItemDefeito == codigo);
        }

        public PagedList<ItemDefeito> ObterPorParametros(ItemDefeitoParameters parameters)
        {
            var query = _context.ItemDefeito
                .Include(or => or.ORDefeito)
                .Include(or => or.ORItem)
                .Include(or => or.Usuario)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodItemDefeito.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodItemDefeito.HasValue)
            {
                query = query.Where(q => q.CodItemDefeito == parameters.CodItemDefeito);
            }

            if (parameters.CodORItem.HasValue)
            {
                query = query.Where(q => q.CodORItem == parameters.CodORItem);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ItemDefeito>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
