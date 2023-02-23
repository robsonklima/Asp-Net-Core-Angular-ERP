using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoStatusRepository
    {
        void Criar(InstalacaoStatus instalLote);
        PagedList<InstalacaoStatus> ObterPorParametros(InstalacaoStatusParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoStatus instalLote);
        InstalacaoStatus ObterPorCodigo(int codigo);
    }
}
