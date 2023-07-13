using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface INavegacaoRepository
    {
        PagedList<Navegacao> ObterPorParametros(NavegacaoParameters parameters);
        void Criar(Navegacao navegacao);
        void Deletar(int codigo);
        void Atualizar(Navegacao navegacao);
        Navegacao ObterPorCodigo(int codigo);
    }
}
