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
    public class ItemSolucaoRepository : IItemSolucaoRepository
    {
        private readonly AppDbContext _context;

        public ItemSolucaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ItemSolucao itemSolucao)
        {
            _context.ChangeTracker.Clear();
            ItemSolucao p = _context.ItemSolucao.FirstOrDefault(p => p.CodItemSolucao == itemSolucao.CodItemSolucao);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(itemSolucao);
                _context.SaveChanges();
            }
        }

        public ItemSolucao Criar(ItemSolucao itemSolucao)
        {
            _context.Add(itemSolucao);
            _context.SaveChanges();
            return itemSolucao;
        }

        public void Deletar(int codigo)
        {
            ItemSolucao ItemSolucao = _context.ItemSolucao.FirstOrDefault(p => p.CodItemSolucao == codigo);

            if (ItemSolucao != null)
            {
                _context.ItemSolucao.Remove(ItemSolucao);
                _context.SaveChanges();
            }
        }

        public ItemSolucao ObterPorCodigo(int codigo)
        {
            return _context.ItemSolucao
                .Include(or => or.ORSolucao)
                .Include(or => or.ORItem)
                .Include(or => or.Usuario)
                .FirstOrDefault(p => p.CodItemSolucao == codigo);
        }

        public PagedList<ItemSolucao> ObterPorParametros(ItemSolucaoParameters parameters)
        {
            var query = _context.ItemSolucao
                .Include(or => or.ORSolucao)
                .Include(or => or.ORItem)
                .Include(or => or.Usuario)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodItemSolucao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodItemSolucao.HasValue)
            {
                query = query.Where(q => q.CodItemSolucao == parameters.CodItemSolucao);
            }

            if (parameters.CodORItem.HasValue)
            {
                query = query.Where(q => q.CodORItem == parameters.CodORItem);
            }

            if(parameters.CodSolucao.HasValue)
            {
                query = query.Where(q => q.CodSolucao == parameters.CodSolucao);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ItemSolucao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
