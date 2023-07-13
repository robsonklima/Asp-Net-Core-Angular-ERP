using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface INavegacaoConfiguracaoService
    {
        ListViewModel ObterPorParametros(NavegacaoConfiguracaoParameters parameters);
        NavegacaoConfiguracao Criar(NavegacaoConfiguracao navegacaoConfiguracao);
        void Deletar(int codigo);
        void Atualizar(NavegacaoConfiguracao navegacaoConfiguracao);
        NavegacaoConfiguracao ObterPorCodigo(int codigo);
    }
}
