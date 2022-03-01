using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepo;

        public NotificacaoService(INotificacaoRepository notificacaoRepo)
        {
            _notificacaoRepo = notificacaoRepo;
        }

        public ListViewModel ObterPorParametros(NotificacaoParameters parameters)
        {
            var notificacoes = _notificacaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = notificacoes,
                TotalCount = notificacoes.TotalCount,
                CurrentPage = notificacoes.CurrentPage,
                PageSize = notificacoes.PageSize,
                TotalPages = notificacoes.TotalPages,
                HasNext = notificacoes.HasNext,
                HasPrevious = notificacoes.HasPrevious
            };

            return lista;
        }

        public Notificacao Criar(Notificacao notificacao)
        {
            _notificacaoRepo.Criar(notificacao);
            return notificacao;
        }

        public void Deletar(int codigo)
        {
            _notificacaoRepo.Deletar(codigo);
        }

        public void Atualizar(Notificacao notificacao)
        {
            _notificacaoRepo.Atualizar(notificacao);
        }

        public Notificacao ObterPorCodigo(int codigo)
        {
            return _notificacaoRepo.ObterPorCodigo(codigo);
        }
    }
}
