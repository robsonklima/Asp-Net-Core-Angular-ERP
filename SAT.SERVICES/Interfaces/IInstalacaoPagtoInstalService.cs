using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoPagtoInstalService
    {
        ListViewModel ObterPorParametros(InstalacaoPagtoInstalParameters parameters);
        InstalacaoPagtoInstal Criar(InstalacaoPagtoInstal instalacaoPagtoInstal);
        void Deletar(int codInstalacao, int codInstalPagto, int codInstalTipoParcela);
        void Atualizar(InstalacaoPagtoInstal instalacaoPagtoInstal);
        InstalacaoPagtoInstal ObterPorCodigo(int codInstalacao, int codInstalPagto, int codInstalTipoParcela);
    }
}