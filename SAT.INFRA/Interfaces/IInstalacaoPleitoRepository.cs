using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoPleitoRepository
    {
        void Criar(InstalacaoPleito instalacaoPleito);
        PagedList<InstalacaoPleito> ObterPorParametros(InstalacaoPleitoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoPleito instalacaoPleito);
        InstalacaoPleito ObterPorCodigo(int codigo);
    }
}
