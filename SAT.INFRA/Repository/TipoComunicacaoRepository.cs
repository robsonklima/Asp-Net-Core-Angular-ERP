using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TipoComunicacaoRepository : ITipoComunicacaoRepository
    {
        private readonly AppDbContext _context;

        public TipoComunicacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public TipoComunicacao Atualizar(TipoComunicacao TipoComunicacao)
        {
            _context.ChangeTracker.Clear();
            TipoComunicacao tc = _context.TipoComunicacao.FirstOrDefault(tc => tc.CodTipoComunicacao == TipoComunicacao.CodTipoComunicacao);

            if (tc != null)
            {
                _context.Entry(tc).CurrentValues.SetValues(TipoComunicacao);
                _context.SaveChanges();
            }

            return tc;
        }

        public TipoComunicacao Criar(TipoComunicacao tipo)
        {
            _context.Add(tipo);
            _context.SaveChanges();
            return tipo;
        }

        public TipoComunicacao Deletar(int codTipoComunicacao)
        {
            TipoComunicacao tc = _context.TipoComunicacao.FirstOrDefault(tc => tc.CodTipoComunicacao == codTipoComunicacao);

            if (tc != null)
            {
                _context.TipoComunicacao.Remove(tc);
                _context.SaveChanges();
            }

            return tc;
        }

        public TipoComunicacao ObterPorCodigo(int codigo)
        {
            return _context.TipoComunicacao.FirstOrDefault(tc => tc.CodTipoComunicacao == codigo);
        }

        public PagedList<TipoComunicacao> ObterPorParametros(TipoComunicacaoParameters parameters)
        {
            var tipos = _context.TipoComunicacao.AsQueryable();

            if (parameters.Filter != null)
            {
                tipos = tipos.Where(
                            t =>
                            t.Tipo.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodTipoComunicacao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            return PagedList<TipoComunicacao>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
