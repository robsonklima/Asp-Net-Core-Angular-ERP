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
    public class ORItemRepository : IORItemRepository
    {
        private readonly AppDbContext _context;

        public ORItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORItem item)
        {
            _context.ChangeTracker.Clear();
            ORItem p = _context.ORItem.FirstOrDefault(p => p.CodORItem == item.CodORItem);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
        }

        public void Criar(ORItem item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public void Deletar(int cod)
        {
            ORItem item = _context.ORItem.FirstOrDefault(p => p.CodORItem == cod);

            if (item != null)
            {
                _context.ORItem.Remove(item);
                _context.SaveChanges();
            }
        }

        public ORItem ObterPorCodigo(int codigo)
        {
            return _context.ORItem
                .Include(or => or.Peca)
                .Include(or => or.UsuarioTecnico)
                .Include(or => or.Cliente)
                .Include(or => or.OrdemServico.Filial)
                .Include(or => or.StatusOR)
                .FirstOrDefault(p => p.CodORItem == codigo);
        }

        public PagedList<ORItem> ObterPorParametros(ORItemParameters parameters)
        {
            var query = _context.ORItem
                .Include(or => or.Peca)
                .Include(or => or.UsuarioTecnico)
                .Include(or => or.Cliente)
                .Include(or => or.OrdemServico.Filial)
                .Include(or => or.StatusOR)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodORItem.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodOR.HasValue) 
            {
                query = query.Where(or => or.CodOR == parameters.CodOR);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORItem>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
