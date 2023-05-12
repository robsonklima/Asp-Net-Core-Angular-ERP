using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class MotivoCancelamentoRepository : IMotivoCancelamentoRepository
    {
        private readonly AppDbContext _context;
        public MotivoCancelamentoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public MotivoCancelamento Atualizar(MotivoCancelamento mc)
        {
            _context.ChangeTracker.Clear();
            MotivoCancelamento t = _context.MotivoCancelamento.FirstOrDefault(t => t.CodMotivoCancelamento == mc.CodMotivoCancelamento);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(mc);
                _context.SaveChanges();
            }

            return t;
        }

        public MotivoCancelamento Criar(MotivoCancelamento mc)
        {
            _context.Add(mc);
            _context.SaveChanges();
            return mc;
        }

        public MotivoCancelamento Deletar(int codMotivoCancelamento)
        {
            MotivoCancelamento t = _context.MotivoCancelamento.FirstOrDefault(t => t.CodMotivoCancelamento == codMotivoCancelamento);

            if (t != null)
            {
                _context.MotivoCancelamento.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public MotivoCancelamento ObterPorCodigo(int codigo)
        {
            return _context.MotivoCancelamento
                .FirstOrDefault(t => t.CodMotivoCancelamento == codigo);
        }

        public PagedList<MotivoCancelamento> ObterPorParametros(MotivoCancelamentoParameters parameters)
        {
            var query = _context.MotivoCancelamento.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<MotivoCancelamento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
