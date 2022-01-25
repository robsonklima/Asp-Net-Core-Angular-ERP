using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TipoContratoRepository : ITipoContratoRepository
    {
        private readonly AppDbContext _context;

        public TipoContratoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(TipoContrato tipoContrato)
        {
            _context.ChangeTracker.Clear();
            TipoContrato tc = _context.TipoContrato.FirstOrDefault(tc => tc.CodTipoContrato == tipoContrato.CodTipoContrato);

            if (tc != null)
            {
                _context.Entry(tc).CurrentValues.SetValues(tipoContrato);
                _context.SaveChanges();
            }
        }

        public void Criar(TipoContrato tipoContrato)
        {
            _context.Add(tipoContrato);
            _context.SaveChanges();
        }

        public void Deletar(int codTipoContrato)
        {
            TipoContrato tc = _context.TipoContrato.FirstOrDefault(tc => tc.CodTipoContrato == codTipoContrato);

            if (tc != null)
            {
                _context.TipoContrato.Remove(tc);
                _context.SaveChanges();
            }
        }

        public TipoContrato ObterPorCodigo(int codigo)
        {
            return _context.TipoContrato.FirstOrDefault(tc => tc.CodTipoContrato == codigo);
        }

        public PagedList<TipoContrato> ObterPorParametros(TipoContratoParameters parameters)
        {
            var tipos = _context.TipoContrato.AsQueryable();

            if (parameters.Filter != null)
            {
                tipos = tipos.Where(
                            t =>
                            t.NomeTipoContrato.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodTipoContrato.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.NomeTipoContrato != null)
            {
                tipos = tipos.Where(t => t.NomeTipoContrato == parameters.NomeTipoContrato);
            }

            if (parameters.CodTipoContrato != null)
            {
                tipos = tipos.Where(t => t.CodTipoContrato == parameters.CodTipoContrato);
            }

            return PagedList<TipoContrato>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
