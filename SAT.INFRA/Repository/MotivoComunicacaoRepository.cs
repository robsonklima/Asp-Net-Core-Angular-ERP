using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class MotivoComunicacaoRepository : IMotivoComunicacaoRepository
    {
        private readonly AppDbContext _context;
        public MotivoComunicacaoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public MotivoComunicacao Atualizar(MotivoComunicacao mc)
        {
            _context.ChangeTracker.Clear();
            MotivoComunicacao t = _context.MotivoComunicacao.FirstOrDefault(t => t.CodMotivoComunicacao == mc.CodMotivoComunicacao);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(mc);
                _context.SaveChanges();
            }

            return t;
        }

        public MotivoComunicacao Criar(MotivoComunicacao mc)
        {
            _context.Add(mc);
            _context.SaveChanges();
            return mc;
        }

        public MotivoComunicacao Deletar(int codMotivoComunicacao)
        {
            MotivoComunicacao t = _context.MotivoComunicacao.FirstOrDefault(t => t.CodMotivoComunicacao == codMotivoComunicacao);

            if (t != null)
            {
                _context.MotivoComunicacao.Remove(t);
                _context.SaveChanges();
            }

            return t;
        }

        public MotivoComunicacao ObterPorCodigo(int codigo)
        {
            return _context.MotivoComunicacao
                .FirstOrDefault(t => t.CodMotivoComunicacao == codigo);
        }

        public PagedList<MotivoComunicacao> ObterPorParametros(MotivoComunicacaoParameters parameters)
        {
            var query = _context.MotivoComunicacao.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<MotivoComunicacao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
