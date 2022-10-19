using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMensagemRepository
    {
        PagedList<Mensagem> ObterPorParametros(MensagemParameters parameters);
        void Criar(Mensagem mensagem);
        void Atualizar(Mensagem mensagem);
        void Deletar(int codigo);
        Mensagem ObterPorCodigo(int codigo);
    }
}
