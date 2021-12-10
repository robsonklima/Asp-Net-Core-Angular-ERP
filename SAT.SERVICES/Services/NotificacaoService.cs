using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _motivoRepo;

        public NotificacaoService(INotificacaoRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(NotificacaoParameters parameters)
        {
            var regioes = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public Notificacao Criar(Notificacao notificacao)
        {
            _motivoRepo.Criar(notificacao);
            return notificacao;
        }

        public void Deletar(int codigo)
        {
            _motivoRepo.Deletar(codigo);
        }

        public void Atualizar(Notificacao notificacao)
        {
            _motivoRepo.Atualizar(notificacao);
        }

        public Notificacao ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
