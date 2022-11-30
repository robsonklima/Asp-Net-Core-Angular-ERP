using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoPleitoService
    {
        ListViewModel ObterPorParametros(InstalacaoPleitoParameters parameters);
        InstalacaoPleito Criar(InstalacaoPleito instalacaoPleito);
        void Deletar(int codigo);
        void Atualizar(InstalacaoPleito instalacaoPleito);
        InstalacaoPleito ObterPorCodigo(int codigo);
    }
}