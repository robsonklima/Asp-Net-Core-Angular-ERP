using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class TipoIntervencaoRepository : ITipoIntervencaoRepository
    {
        private readonly AppDbContext _context;

        public TipoIntervencaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(TipoIntervencao tipoIntervencao)
        {
            _context.ChangeTracker.Clear();
            TipoIntervencao ti = _context.TipoIntervencao.FirstOrDefault(ti => ti.CodTipoIntervencao == tipoIntervencao.CodTipoIntervencao);

            if (ti != null)
            {
                _context.Entry(ti).CurrentValues.SetValues(tipoIntervencao);
                _context.SaveChanges();
            }
        }

        public void Criar(TipoIntervencao tipoIntervencao)
        {
            _context.Add(tipoIntervencao);
            _context.SaveChanges();
        }

        public void Deletar(int codTipointervencao)
        {
            TipoIntervencao ti = _context.TipoIntervencao.FirstOrDefault(ti => ti.CodTipoIntervencao == codTipointervencao);

            if (ti != null)
            {
                _context.TipoIntervencao.Remove(ti);
                _context.SaveChanges();
            }
        }

        public TipoIntervencao ObterPorCodigo(int codigo)
        {
            return _context.TipoIntervencao.FirstOrDefault(ti => ti.CodTipoIntervencao == codigo);
        }

        public PagedList<TipoIntervencao> ObterPorParametros(TipoIntervencaoParameters parameters)
        {
            var tipos = _context.TipoIntervencao.AsQueryable();

            if (parameters.Filter != null)
            {
                tipos = tipos.Where(
                            t =>
                            t.NomTipoIntervencao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodTipoIntervencao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodTipoIntervencao != null)
            {
                tipos = tipos.Where(t => t.CodTipoIntervencao == parameters.CodTipoIntervencao);
            }

            if (parameters.IndAtivo != null)
            {
                tipos = tipos.Where(t => t.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodTiposIntervencao))
            {
                int[] cods = parameters.CodTiposIntervencao.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                tipos = tipos.Where(t => cods.Contains(t.CodTipoIntervencao.Value));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tipos = tipos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<TipoIntervencao>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
