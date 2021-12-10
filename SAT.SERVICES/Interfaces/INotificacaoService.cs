using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface INotificacaoService
    {
        ListViewModel ObterPorParametros(NotificacaoParameters parameters);
        Notificacao Criar(Notificacao notificacao);
        void Deletar(int codigo);
        void Atualizar(Notificacao notificacao);
        Notificacao ObterPorCodigo(int codigo);
    }
}
