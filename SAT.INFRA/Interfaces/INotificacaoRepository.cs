using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface INotificacaoRepository
    {
        PagedList<Notificacao> ObterPorParametros(NotificacaoParameters parameters);
        void Criar(Notificacao notificacao);
        void Atualizar(Notificacao notificacao);
        void Deletar(int codNotificacao);
        Notificacao ObterPorCodigo(int codigo);
    }
}
