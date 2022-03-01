using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TipoIndiceReajusteRepository : ITipoIndiceReajusteRepository
    {
        private readonly AppDbContext _context;

        public TipoIndiceReajusteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(TipoIndiceReajuste tipoIndiceReajuste)
        {
            _context.ChangeTracker.Clear();
            TipoIndiceReajuste tc = _context.TipoIndiceReajuste.FirstOrDefault(tc => tc.CodTipoIndiceReajuste == tipoIndiceReajuste.CodTipoIndiceReajuste);

            if (tc != null)
            {
                _context.Entry(tc).CurrentValues.SetValues(tipoIndiceReajuste);
                _context.SaveChanges();
            }
        }

        public void Criar(TipoIndiceReajuste tipoIndiceReajuste)
        {
            _context.Add(tipoIndiceReajuste);
            _context.SaveChanges();
        }

        public void Deletar(int codTipoIndiceReajuste)
        {
            TipoIndiceReajuste tc = _context.TipoIndiceReajuste.FirstOrDefault(tc => tc.CodTipoIndiceReajuste == codTipoIndiceReajuste);

            if (tc != null)
            {
                _context.TipoIndiceReajuste.Remove(tc);
                _context.SaveChanges();
            }
        }

        public TipoIndiceReajuste ObterPorCodigo(int codigo)
        {
            return _context.TipoIndiceReajuste.FirstOrDefault(tc => tc.CodTipoIndiceReajuste == codigo);
        }

        public PagedList<TipoIndiceReajuste> ObterPorParametros(TipoIndiceReajusteParameters parameters)
        {
            var tipos = _context.TipoIndiceReajuste.AsQueryable();

            if (parameters.Filter != null)
            {
                tipos = tipos.Where(
                            t =>
                            t.NomeTipoIndiceReajuste.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodTipoIndiceReajuste.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.NomeTipoIndiceReajuste != null)
            {
                tipos = tipos.Where(t => t.NomeTipoIndiceReajuste == parameters.NomeTipoIndiceReajuste);
            }

            if (parameters.CodTipoIndiceReajuste != null)
            {
                tipos = tipos.Where(t => t.CodTipoIndiceReajuste == parameters.CodTipoIndiceReajuste);
            }

            return PagedList<TipoIndiceReajuste>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
