using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoPleitoInstalService
    {
        ListViewModel ObterPorParametros(InstalacaoPleitoInstalParameters parameters);
        InstalacaoPleitoInstal Criar(InstalacaoPleitoInstal instalacaoPleitoInstal);
        void Deletar(int codInstalacao, int codInstalPleito);
        void Atualizar(InstalacaoPleitoInstal instalacaoPleitoInstal);
        InstalacaoPleitoInstal ObterPorCodigo(int codInstalacao, int codInstalPleito);
    }
}