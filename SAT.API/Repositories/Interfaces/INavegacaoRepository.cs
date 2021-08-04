using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface INavegacaoRepository
    {
        void Criar(Navegacao navegacao);
        PagedList<Navegacao> ObterPorParametros(NavegacaoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(Navegacao navegacao);
        Navegacao ObterPorCodigo(int codigo);
    }
}
