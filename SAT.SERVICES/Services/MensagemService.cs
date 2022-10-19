using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MensagemService : IMensagemService
    {
        private readonly IMensagemRepository _mensagemRepo;

        public MensagemService(IMensagemRepository mensagemRepo)
        {
            _mensagemRepo = mensagemRepo;
        }

        public ListViewModel ObterPorParametros(MensagemParameters parameters)
        {
            var notificacoes = _mensagemRepo.ObterPorParametros(parameters);

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

        public Mensagem Criar(Mensagem Mensagem)
        {
            _mensagemRepo.Criar(Mensagem);
            return Mensagem;
        }

        public void Deletar(int codigo)
        {
            _mensagemRepo.Deletar(codigo);
        }

        public void Atualizar(Mensagem Mensagem)
        {
            _mensagemRepo.Atualizar(Mensagem);
        }

        public Mensagem ObterPorCodigo(int codigo)
        {
            return _mensagemRepo.ObterPorCodigo(codigo);
        }
    }
}
