using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface INavegacaoConfiguracaoTipoService
    {
        ListViewModel ObterPorParametros(NavegacaoConfiguracaoTipoParameters parameters);
        NavegacaoConfiguracaoTipo Criar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo);
        void Deletar(int codigo);
        void Atualizar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo);
        NavegacaoConfiguracaoTipo ObterPorCodigo(int codigo);
    }
}
