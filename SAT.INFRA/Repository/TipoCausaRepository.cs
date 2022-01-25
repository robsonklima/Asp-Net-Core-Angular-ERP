using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TipoCausaRepository : ITipoCausaRepository
    {
        private readonly AppDbContext _context;

        public TipoCausaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(TipoCausa tipoCausa)
        {
            _context.ChangeTracker.Clear();
            TipoCausa tc = _context.TipoCausa.FirstOrDefault(tc => tc.CodTipoCausa == tipoCausa.CodTipoCausa);

            if (tc != null)
            {
                _context.Entry(tc).CurrentValues.SetValues(tipoCausa);
                _context.SaveChanges();
            }
        }

        public void Criar(TipoCausa tipoCausa)
        {
            _context.Add(tipoCausa);
            _context.SaveChanges();
        }

        public void Deletar(int codTipoCausa)
        {
            TipoCausa tc = _context.TipoCausa.FirstOrDefault(tc => tc.CodTipoCausa == codTipoCausa);

            if (tc != null)
            {
                _context.TipoCausa.Remove(tc);
                _context.SaveChanges();
            }
        }

        public TipoCausa ObterPorCodigo(int codigo)
        {
            return _context.TipoCausa.FirstOrDefault(tc => tc.CodTipoCausa == codigo);
        }

        public PagedList<TipoCausa> ObterPorParametros(TipoCausaParameters parameters)
        {
            var tipos = _context.TipoCausa.AsQueryable();

            if (parameters.Filter != null)
            {
                tipos = tipos.Where(
                            t =>
                            t.NomeTipoCausa.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodETipoCausa.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodTipoCausa.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodETipoCausa != null)
            {
                tipos = tipos.Where(t => t.CodETipoCausa == parameters.CodETipoCausa);
            }

            if (parameters.CodTipoCausa != null)
            {
                tipos = tipos.Where(t => t.CodTipoCausa == parameters.CodTipoCausa);
            }

            if (parameters.IndAtivo != null)
            {
                tipos = tipos.Where(t => t.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tipos = tipos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<TipoCausa>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
