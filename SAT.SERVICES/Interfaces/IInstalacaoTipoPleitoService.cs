using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoTipoPleitoService
    {
        ListViewModel ObterPorParametros(InstalacaoTipoPleitoParameters parameters);
        InstalacaoTipoPleito Criar(InstalacaoTipoPleito instalacaoTipoPleito);
        void Deletar(int codigo);
        void Atualizar(InstalacaoTipoPleito instalacaoTipoPleito);
        InstalacaoTipoPleito ObterPorCodigo(int codigo);
    }
}