using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface INavegacaoConfiguracaoRepository
    {
        PagedList<NavegacaoConfiguracao> ObterPorParametros(NavegacaoConfiguracaoParameters parameters);
        void Criar(NavegacaoConfiguracao navegacaoConfiguracao);
        void Deletar(int codigo);
        void Atualizar(NavegacaoConfiguracao navegacaoConfiguracao);
        NavegacaoConfiguracao ObterPorCodigo(int codigo);
    }
}
