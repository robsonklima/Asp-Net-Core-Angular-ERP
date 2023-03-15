using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoPagtoInstalRepository
    {
        void Criar(InstalacaoPagtoInstal instalacaoPagtoInstal);
        PagedList<InstalacaoPagtoInstal> ObterPorParametros(InstalacaoPagtoInstalParameters parameters);
        void Deletar(int codInstalacao, int codInstalPagto);
        void Atualizar(InstalacaoPagtoInstal instalacaoPagtoInstal);
        InstalacaoPagtoInstal ObterPorCodigo(int codInstalacao, int codInstalPagto);
    }
}
