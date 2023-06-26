using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class SatTaskProcessoRepository : ISatTaskProcessoRepository
    {
        private readonly AppDbContext _context;
        public SatTaskProcessoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public SatTaskProcesso Atualizar(SatTaskProcesso processo)
        {
            _context.ChangeTracker.Clear();
            SatTaskProcesso t = _context.SatTaskProcesso.FirstOrDefault(t => t.CodSatTaskProcesso == processo.CodSatTaskProcesso);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(processo);
                _context.SaveChanges();
            }

            return t;
        }

        public SatTaskProcesso Criar(SatTaskProcesso processo)
        {
            _context.Add(processo);
            _context.SaveChanges();
            return processo;
        }

        public SatTaskProcesso Deletar(int codSatTaskProcesso)
        {
            SatTaskProcesso t = _context.SatTaskProcesso.FirstOrDefault(t => t.CodSatTaskProcesso == codSatTaskProcesso);

            if (t != null)
            {
                _context.SatTaskProcesso.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public SatTaskProcesso ObterPorCodigo(int codigo)
        {
            return _context.SatTaskProcesso
                .Include(t => t.Tipo)
                .FirstOrDefault(t => t.CodSatTaskProcesso == codigo);
        }

        public PagedList<SatTaskProcesso> ObterPorParametros(SatTaskProcessoParameters parameters)
        {
            var query = _context.SatTaskProcesso
                .Include(t => t.Tipo)
                .AsQueryable();

            if (parameters.CodSatTaskTipo.HasValue) {
                query = query.Where(t => t.CodSatTaskTipo == (int)parameters.CodSatTaskTipo);
            }

            if (parameters.CodOS == parameters.CodOS) {
                query = query.Where(t => t.CodOS == parameters.CodOS);
            } 

            if (!string.IsNullOrWhiteSpace(parameters.Status))
            {
                query = query.Where(t => t.Status == parameters.Status);
            }

            var q = query.ToQueryString();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<SatTaskProcesso>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
