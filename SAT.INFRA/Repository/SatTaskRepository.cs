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
    public partial class SatTaskRepository : ISatTaskRepository
    {
        private readonly AppDbContext _context;
        public SatTaskRepository(AppDbContext context)
        {
            this._context = context;
        }

        public SatTask Atualizar(SatTask SatTask)
        {
            _context.ChangeTracker.Clear();
            SatTask t = _context.SatTask.FirstOrDefault(t => t.CodSatTask == SatTask.CodSatTask);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(SatTask);
                _context.SaveChanges();
            }

            return t;
        }

        public SatTask Criar(SatTask SatTask)
        {
            _context.Add(SatTask);
            _context.SaveChanges();
            return SatTask;
        }

        public SatTask Deletar(int codSatTask)
        {
            SatTask t = _context.SatTask.FirstOrDefault(t => t.CodSatTask == codSatTask);

            if (t != null)
            {
                _context.SatTask.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public SatTask ObterPorCodigo(int codigo)
        {
            return _context.SatTask.FirstOrDefault(t => t.CodSatTask == codigo);
        }

        public PagedList<SatTask> ObterPorParametros(SatTaskParameters parameters)
        {
            var query = _context.SatTask.AsQueryable();

            if (parameters.CodSatTaskTipo > 0) {
                query = query.Where(t => t.CodSatTaskTipo == (int)parameters.CodSatTaskTipo);
            }

            if (parameters.IndProcessado == Constants.ATIVO) {
                query = query.Where(t => t.IndProcessado == Constants.ATIVO);
            } 
            else 
            {
                query = query.Where(t => t.IndProcessado == Constants.INATIVO);
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<SatTask>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
