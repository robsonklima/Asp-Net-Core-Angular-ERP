using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface INavegacaoConfiguracaoTipoRepository
    {
        PagedList<NavegacaoConfiguracaoTipo> ObterPorParametros(NavegacaoConfiguracaoTipoParameters parameters);
        void Criar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo);
        void Deletar(int codigo);
        void Atualizar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo);
        NavegacaoConfiguracaoTipo ObterPorCodigo(int codigo);
    }
}
