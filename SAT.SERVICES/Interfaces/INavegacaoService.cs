using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface INavegacaoService
    {
        ListViewModel ObterPorParametros(NavegacaoParameters parameters);
        Navegacao Criar(Navegacao navegacao);
        void Deletar(int codigo);
        void Atualizar(Navegacao navegacao);
        Navegacao ObterPorCodigo(int codigo);
    }
}
