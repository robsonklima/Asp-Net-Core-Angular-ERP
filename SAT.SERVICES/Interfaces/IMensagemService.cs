using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IMensagemService
    {
        ListViewModel ObterPorParametros(MensagemParameters parameters);
        Mensagem Criar(Mensagem mensagem);
        void Deletar(int codigo);
        void Atualizar(Mensagem mensagem);
        Mensagem ObterPorCodigo(int codigo);
    }
}
