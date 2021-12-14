using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly AppDbContext _context;

        public NotificacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Notificacao notificacao)
        {
            Notificacao p = _context.Notificacao.FirstOrDefault(p => p.CodNotificacao == notificacao.CodNotificacao);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(notificacao);
                _context.SaveChanges();
            }
        }

        public void Criar(Notificacao notificacao)
        {
            _context.Add(notificacao);
            _context.SaveChanges();
        }

        public void Deletar(int codNotificacao)
        {
            Notificacao notificacao = _context.Notificacao.FirstOrDefault(p => p.CodNotificacao == codNotificacao);

            if (notificacao != null)
            {
                _context.Notificacao.Remove(notificacao);
                _context.SaveChanges();
            }
        }

        public Notificacao ObterPorCodigo(int codigo)
        {
            return _context.Notificacao.FirstOrDefault(p => p.CodNotificacao == codigo);
        }

        public PagedList<Notificacao> ObterPorParametros(NotificacaoParameters parameters)
        {
            var notificacaoes = _context.Notificacao.AsQueryable();

            if (parameters.Filter != null)
            {
                notificacaoes = notificacaoes.Where(
                            p =>
                            p.CodNotificacao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            p.Titulo.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            p.Descricao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                            
                );
            }

            if (parameters.CodUsuario != null)
            {
                notificacaoes = notificacaoes.Where(n => n.CodUsuario == parameters.CodUsuario);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                notificacaoes = notificacaoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Notificacao>.ToPagedList(notificacaoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
