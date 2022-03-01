using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoLoteRepository
    {
        void Criar(InstalacaoLote instalLote);
        PagedList<InstalacaoLote> ObterPorParametros(InstalacaoLoteParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoLote instalLote);
        InstalacaoLote ObterPorCodigo(int codigo);
    }
}
