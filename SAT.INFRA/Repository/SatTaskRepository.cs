using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class SatTaskRepository : ISatTaskRepository
    {
        private readonly AppDbContext _context;
        public SatTaskRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Atualizar(SatTask SatTask)
        {
            _context.ChangeTracker.Clear();
            SatTask t = _context.SatTask.FirstOrDefault(t => t.CodSatTask == SatTask.CodSatTask);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(SatTask);
                _context.SaveChanges();
            }
        }

        public void Criar(SatTask SatTask)
        {
            _context.Add(SatTask);
            _context.SaveChanges();
        }

        public void Deletar(int codSatTask)
        {
            SatTask t = _context.SatTask.FirstOrDefault(t => t.CodSatTask == codSatTask);

            if (t != null)
            {
                _context.SatTask.Remove(t);
                _context.SaveChanges();
            }
        }

        public SatTask ObterPorCodigo(int codigo)
        {
            return _context.SatTask
                .FirstOrDefault(t => t.CodSatTask == codigo);
        }

        public PagedList<SatTask> ObterPorParametros(SatTaskParameters parameters)
        {
            var query = _context.SatTask.AsQueryable();

            if (parameters.CodSatTaskTipo.HasValue) {
                query = query.Where(t => t.codSatTaskTipo == parameters.CodSatTaskTipo);
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<SatTask>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
