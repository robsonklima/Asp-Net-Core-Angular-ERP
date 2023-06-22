using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using SAT.MODELS.Entities.Constants;

namespace SAT.INFRA.Repository
{
    public partial class SatTaskTipoRepository : ISatTaskTipoRepository
    {
        private readonly AppDbContext _context;
        public SatTaskTipoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Atualizar(SatTaskTipo SatTaskTipo)
        {
            _context.ChangeTracker.Clear();
            SatTaskTipo t = _context.SatTaskTipo.FirstOrDefault(t => t.CodSatTaskTipo == SatTaskTipo.CodSatTaskTipo);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(SatTaskTipo);
                _context.SaveChanges();
            }
        }

        public void Criar(SatTaskTipo SatTaskTipo)
        {
            _context.Add(SatTaskTipo);
            _context.SaveChanges();
        }

        public void Deletar(int codSatTaskTipo)
        {
            SatTaskTipo t = _context.SatTaskTipo.FirstOrDefault(t => t.CodSatTaskTipo == codSatTaskTipo);

            if (t != null)
            {
                _context.SatTaskTipo.Remove(t);
                _context.SaveChanges();
            }
        }

        public SatTaskTipo ObterPorCodigo(int codigo)
        {
            return _context.SatTaskTipo
                .FirstOrDefault(t => t.CodSatTaskTipo == codigo);
        }

        public PagedList<SatTaskTipo> ObterPorParametros(SatTaskTipoParameters parameters)
        {
            var query = _context.SatTaskTipo.AsQueryable();

            if (parameters.IndAtivo == Constants.ATIVO)
                query = query.Where(t => t.IndAtivo == Constants.ATIVO);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<SatTaskTipo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
