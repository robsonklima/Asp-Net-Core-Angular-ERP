using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoPleitoInstalRepository
    {
        void Criar(InstalacaoPleitoInstal instalacaoPleitoInstal);
        PagedList<InstalacaoPleitoInstal> ObterPorParametros(InstalacaoPleitoInstalParameters parameters);
        void Deletar(int codInstalacao, int CodInstalPleito);
        void Atualizar(InstalacaoPleitoInstal instalacaoPleitoInstal);
    }
}
