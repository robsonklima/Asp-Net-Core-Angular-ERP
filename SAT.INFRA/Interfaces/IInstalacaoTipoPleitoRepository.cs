using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoTipoPleitoRepository
    {
        void Criar(InstalacaoTipoPleito instalacaoTipoPleito);
        PagedList<InstalacaoTipoPleito> ObterPorParametros(InstalacaoTipoPleitoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoTipoPleito instalacaoTipoPleito);
        InstalacaoTipoPleito ObterPorCodigo(int codigo);
    }
}
